using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITempoCursor : MonoBehaviour
{
    [SerializeField] private ShootPlayer player;
    [SerializeField] private TempoManager tempo;
    private Material material;


    [SerializeField] private int beatLead = 1;
    [SerializeField] private GameObject TempoGuidePrefab;

    // Avec ces 3 fields on peut facilement prendre et assigner des valeurs aux variable importantes du material
    public float PerfectMargin { get => material.GetFloat("perfectMargin"); set => material.SetFloat("perfectMargin", value); }
    public float OkayMargin { get => material.GetFloat("okMargin"); set => material.SetFloat("okMargin", value); }

    private void Start()
    {
       /* material = GetComponent<Image>().material;
        Cursor.visible = false;

        PerfectMargin = tempo.MarginPerfect;
        OkayMargin = tempo.MarginOk;*/
    }

    private void Update()
    {
        transform.position = player.MouseScreenPosition;
    }

    // J'ai fait ça pour être propre mais c'est pas giga important
    private void OnDestroy()
    {
        material.SetFloat("tempo", 0);
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        material.SetFloat("tempo", 0);
        enabled = false;
        Cursor.visible = true;
    }

    /*public void NewCombo(int combo, int max)
    {
        //faire un calcul en fonction de la surchauffe et de la taille du tir pour que que se soit recalculer à chaque fois
        float margin = tempo.MarginPerfectEvolution.Evaluate((float)combo / (float)max) ;
        OkayMargin = tempo.MarginPerfect + (margin * (tempo.MarginOk - tempo.MarginPerfect));
    }

    public void BeatStart(int beat)
    {
        //Debug.Log($"{beat} => {tempo.PlayerShootBeat[(beat + beatLead) % 4]}");
        if (tempo.PlayerShootBeat[(beat + beatLead) % 4])
        {
            TempoGuide guide = Instantiate(TempoGuidePrefab, transform).GetComponent<TempoGuide>();
            guide.player = player;
            guide.tempo = tempo;
        }
    }*/
}
