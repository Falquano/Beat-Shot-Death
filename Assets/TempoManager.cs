using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TempoManager : MonoBehaviour
{
    //variable pour le tir en tempo
    [SerializeField] private float ObjectiveShoot = 0.5f;
    [SerializeField] private float MarginPerfect = 0.1f;
    [SerializeField] private float MarginOk = 0.3f;
    private float TimerTempo;
    [SerializeField] private float TempoDuration;
    public float Tempo => TimerTempo / TempoDuration;
    [SerializeField] private AnimationCurve MarginPerfectEvolution;

    public float Combo { get; set; }


    [SerializeField] private int mesurePerRound = 4;
    public int Mesure { get; private set; }
    [SerializeField] public UnityEvent<int> onMesureStart = new UnityEvent<int>();
    [SerializeField] public UnityEvent onTimeToShoot = new UnityEvent();

    // Update is called once per frame
    void Update()
    {
        TimerTempo += Time.deltaTime;
        if (TimerTempo >= TempoDuration)
        {
            NouvelleMesure();
        }
        if (TimerTempo - Time.deltaTime < ObjectiveShoot && TimerTempo >= ObjectiveShoot)
        {
            TimeToShoot();
        }

        TimerTempo %= TempoDuration;

        //faire un calcul en fonction de la surchauffe et de la taille du tir pour que que se soit recalculer à chaque fois
        float ChangeValuePerfect = MarginPerfectEvolution.Evaluate(Combo / 100f);

        // FAIRE UN TRUC AVEC CETTE VALEUR
    }

    private void NouvelleMesure()
    {
        //Debug.Log("---\tMesure");
        Mesure = (Mesure + 1) % mesurePerRound;
        onMesureStart.Invoke(Mesure);
    }

    private void TimeToShoot()
    {
        onTimeToShoot.Invoke();
    }

    public ShotQuality ShotQualityNow()
    {
        if (Tempo >= ObjectiveShoot - MarginOk && Tempo <= ObjectiveShoot + MarginOk)
        {
            if (Tempo >= ObjectiveShoot - MarginPerfect && Tempo <= ObjectiveShoot + MarginPerfect)
            {
                return ShotQuality.Perfect;
            }
            return ShotQuality.Okay;
        }
        return ShotQuality.Failed;
    }
}
