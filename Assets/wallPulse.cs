using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallPulse : MonoBehaviour
{
    [SerializeField] private Tempo_MurEmission[] wall;
    //[SerializeField] private Tempo_MurEmission[] lignes;

    // Start is called before the first frame update
    void Start()
    {
        wall = GetComponentsInChildren<Tempo_MurEmission>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pulsing()
    {
        foreach (Tempo_MurEmission walls in wall)
            walls.OnPulse();

        /*foreach (Tempo_MurEmission ligne in lignes)
            ligne.OnPulse();*/
    }
}
