using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnnemyBehavior : MonoBehaviour
{
    private TempoManager tempo;
    [SerializeField] private int EnnemyLife = 100;

    [SerializeField] private Mesure[] mesures;
    private int currentMesure;

    private void Start()
    {
        tempo = FindObjectOfType<TempoManager>();
        tempo.onMesureStart.AddListener(OnNewMesure);
        currentMesure = 0;

        for (int i = 1; i < mesures.Length; i++)
        {
            InitMesure(i);
            SetBehaviorEnabled(i, false);
        }
        InitMesure(0);
        SetBehaviorEnabled(0, true);
    }

    public void OnNewMesure(int newMesure)
    {
        SetBehaviorEnabled(currentMesure, false);
        SetBehaviorEnabled(newMesure, true);
        currentMesure = newMesure;
    }

    private void SetBehaviorEnabled(int index, bool enabled)
    {
        if (mesures[index] != null)
            mesures[index].enabled = enabled;
    }

    private void InitMesure(int index)
    {
        if (mesures[index] != null)
            mesures[index].Init();
    }

    public void DamageEnnemy(int HitLife)
    {
        EnnemyLife -= HitLife;
        
        if(EnnemyLife <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
