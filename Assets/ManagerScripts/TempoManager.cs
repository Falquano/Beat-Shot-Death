using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TempoManager : MonoBehaviour
{
    //variable pour le tir en tempo
    [SerializeField] private float objectiveShoot = 1f;
    public float ObjectiveShoot => objectiveShoot;
    [SerializeField] private float marginPerfect = 1f;
    public float MarginPerfect => marginPerfect;
    [SerializeField] private float marginOk = 0.3f;
    public float MarginOk => marginOk;

    [SerializeField] private bool[] playerShootBeat;
    public bool[] PlayerShootBeat => playerShootBeat;

    private float TimerTempo;
    [Tooltip("Utilis� uniquement si il n'y a pas de musique !")]
    [SerializeField] private float TempoDuration;
    [Tooltip("Musique du niveau")]
    [SerializeField] private Song song;
    [SerializeField] private StudioEventEmitter songEmitter;
    public float Tempo => TimerTempo / TempoDuration;
    //[SerializeField] public AnimationCurve MarginPerfectEvolution;

    public float Combo { get; set; }


    [SerializeField] private int beatPerMesure = 4;
    
    [SerializeField] private int playerBar = 2;
    public int Beat { get; private set; }
    [SerializeField] public UnityEvent<int> onMesureStart = new UnityEvent<int>();
    [SerializeField] public UnityEvent<int> onPlayerBarStart = new UnityEvent<int>();
    [SerializeField] public UnityEvent onTimeToShoot = new UnityEvent();
    [SerializeField] public UnityEvent onPlayerTimeToShoot = new UnityEvent();

	private void Start()
	{
        if (MainMenu.SelectedSong != null)
		{
			song = MainMenu.SelectedSong;
		}
        
        if (song != null)
		{
            TempoDuration = song.TempoLength;
            songEmitter.EventReference = song.SongReference;
            songEmitter.Play();
            TimerTempo = song.Offset;
		}

        if (playerShootBeat == null)
		{
            playerShootBeat = new bool[] { true, false, true, false };
		}

        //TimerTempo = ObjectiveShoot * TempoDuration;
	}

    private void OnDestroy()
    {
        songEmitter.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        TimerTempo += Time.deltaTime;
        if ((TimerTempo - Time.deltaTime) / TempoDuration < objectiveShoot && TimerTempo / TempoDuration >= objectiveShoot)
        {
            TimeToShoot();
        }
        if (TimerTempo >= TempoDuration)
        {
            NouvelleMesure();
        }

        TimerTempo %= TempoDuration;

        marginPerfect = CalculMarginPerfect();
        print(marginPerfect);
    }

    private void NouvelleMesure()
    {
        //Debug.Log("---\tMesure");
        Beat = (Beat + 1) % beatPerMesure; //Va de 0 à 4 (beatPerMesure = 4 inspecteur)
        onMesureStart.Invoke(Beat); //Appelé tous les beats

        if (Beat % playerBar == 0)
        {
            onPlayerBarStart.Invoke(Beat);
        }
    }

    public void NewCombo(int combo, int max)
    {
        //faire un calcul en fonction de la surchauffe et de la taille du tir pour que se soit recalculer � chaque fois
        //float margin = MarginPerfectEvolution.Evaluate((float)combo / (float)max);

        
    }

    private float CalculMarginPerfect()
    {
        if(Combo >= 0 && Combo < 1)
        {
            return (1.0f);
        }
        else if(Combo > 1 && Combo <= 10)
        {
            return (0.84f);
        }
        else if(Combo > 10 && Combo <= 30)
        {
            return (0.68f);
        }
        else if(Combo > 30 && Combo <= 60)
        {
            return (0.54f);
        }
        else
        {
            return (0.34f);
        }
    }

    private void TimeToShoot()
    {
        onTimeToShoot.Invoke();
        if (playerShootBeat[Beat])
            onPlayerTimeToShoot.Invoke();
    }

    

    // Cette version est plus permissive mais ne fonctionne qu'avec objectiveshoot = 1 !
    public ShotQuality ShotQualityNow()
    {
        if (!playerShootBeat[(Beat + 1) % beatPerMesure])
            return ShotQuality.Bad;

        if (Tempo >= objectiveShoot - marginPerfect || Tempo < marginPerfect / 2f)
        {
            //print(
            return ShotQuality.Perfect;

        }
        //else if (Tempo >= objectiveShoot - marginOk)
        //{
        //    return ShotQuality.Good;
        //}
        return ShotQuality.Bad;
    }
}
