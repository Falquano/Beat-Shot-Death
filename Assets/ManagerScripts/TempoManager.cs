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
    [SerializeField] private float marginPerfect = 0.1f;
    public float MarginPerfect => marginPerfect;
    [SerializeField] private float marginOk = 0.3f;
    public float MarginOk => marginOk;

    [SerializeField] private bool[] playerShootBeat;
    public bool[] PlayerShootBeat => playerShootBeat;

    private float TimerTempo;
    [Tooltip("Utilisé uniquement si il n'y a pas de musique !")]
    [SerializeField] private float TempoDuration;
    [Tooltip("Musique du niveau")]
    [SerializeField] private Song song;
    [SerializeField] private StudioEventEmitter songEmitter;
    public float Tempo => TimerTempo / TempoDuration;
    [SerializeField] public AnimationCurve MarginPerfectEvolution;

    public float Combo { get; set; }


    [SerializeField] private int beatPerMesure = 4;
    public int Beat { get; private set; }
    [SerializeField] public UnityEvent<int> onMesureStart = new UnityEvent<int>();
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
    }

    private void NouvelleMesure()
    {
        //Debug.Log("---\tMesure");
        Beat = (Beat + 1) % beatPerMesure;
        onMesureStart.Invoke(Beat);
    }

    public void NewCombo(int combo, int max)
    {
        //faire un calcul en fonction de la surchauffe et de la taille du tir pour que se soit recalculer à chaque fois
        float margin = MarginPerfectEvolution.Evaluate((float)combo / (float)max);
        marginPerfect = margin;
    }

    private void TimeToShoot()
    {
        onTimeToShoot.Invoke();
        if (playerShootBeat[Beat])
            onPlayerTimeToShoot.Invoke();
    }

    /*public ShotQuality ShotQualityNow()
    {
        if (Tempo >= objectiveShoot - marginOk && Tempo <= objectiveShoot + marginOk)
        {
            if (Tempo >= objectiveShoot - marginPerfect && Tempo <= objectiveShoot + marginPerfect)
            {
                return ShotQuality.Perfect;
            }
            return ShotQuality.Good;
        }
        return ShotQuality.Bad;
    }*/

    // Cette version est plus permissive mais ne fonctionne qu'avec objectiveshoot = 1 !
    public ShotQuality ShotQualityNow()
    {
        if (!playerShootBeat[(Beat + 1) % beatPerMesure])
            return ShotQuality.Bad;
        if (Tempo >= objectiveShoot - marginPerfect || Tempo < marginPerfect / 2f)
        {
            return ShotQuality.Perfect;
        }
        else if (Tempo >= objectiveShoot - marginOk)
        {
            return ShotQuality.Good;
        }
        return ShotQuality.Bad;
    }
}
