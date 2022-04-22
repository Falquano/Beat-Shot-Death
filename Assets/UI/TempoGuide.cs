using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempoGuide : MonoBehaviour
{
    public ShootPlayer player { get; set; }
    public TempoManager tempo { get; set; }
    private Material material;
    public float TargetValue { get => material.GetFloat("targetValue"); set => material.SetFloat("targetValue", value); }

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Image>().material;
        Cursor.visible = false;

        TargetValue = tempo.ObjectiveShoot;

        tempo.onMesureStart.AddListener(BeatEnds);
    }

    // Update is called once per frame
    void Update()
    {
        // On met la variable "tempo" du material à jour
        material.SetFloat("tempo", tempo.Tempo);
    }

    public void BeatEnds(int newBeat)
	{
        Destroy(gameObject);
	}

    public void Shot()
	{
        material.SetColor("_GuideColor", Color.yellow);
	}
}
