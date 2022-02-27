using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITempoCursor : MonoBehaviour
{
    [SerializeField] private ShootPlayer player;
    [SerializeField] private TempoManager tempo;
    private Material material;

    // Avec ces 3 fields on peut facilement prendre et assigner des valeurs aux variable importantes du material
    public float TargetValue { get => material.GetFloat("targetValue"); set => material.SetFloat("targetValue", value); }
    public float PerfectMargin { get => material.GetFloat("perfectMargin"); set => material.SetFloat("perfectMargin", value); }
    public float OkayMargin { get => material.GetFloat("okMargin"); set => material.SetFloat("okMargin", value); }

    private void Start()
    {
        material = GetComponent<Image>().material;
        Cursor.visible = false;

        TargetValue = tempo.ObjectiveShoot;
        PerfectMargin = tempo.MarginPerfect;
        OkayMargin = tempo.MarginOk;
    }

    private void Update()
    {
        transform.position = player.MouseScreenPosition;
        // On met la variable "tempo" du material à jour
        material.SetFloat("tempo", tempo.Tempo);
    }

    // J'ai fait ça pour être propre mais c'est pas giga important
    private void OnDestroy()
    {
        material.SetFloat("tempo", 0);
        enabled = false;
    }

    private void OnDisable()
    {
        material.SetFloat("tempo", 0);
        enabled = false;
    }

    public void NewCombo(int combo, int max)
    {
        //faire un calcul en fonction de la surchauffe et de la taille du tir pour que que se soit recalculer à chaque fois
        float margin = tempo.MarginPerfectEvolution.Evaluate((float)combo / (float)max);
        PerfectMargin = margin;
    }
}
