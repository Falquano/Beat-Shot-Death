using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempoGuide : MonoBehaviour
{
    [SerializeField] private ShootPlayer player;
    [SerializeField] private TempoManager tempo;
    private Material material;
    public float TargetValue { get => material.GetFloat("targetValue"); set => material.SetFloat("targetValue", value); }

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Image>().material;
        Cursor.visible = false;

        TargetValue = tempo.ObjectiveShoot;
    }

    // Update is called once per frame
    void Update()
    {
        // On met la variable "tempo" du material à jour
        material.SetFloat("tempo", tempo.Tempo);
    }
}
