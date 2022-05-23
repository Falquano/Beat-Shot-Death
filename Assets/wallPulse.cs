using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallPulse : MonoBehaviour
{
    [SerializeField] private Tempo_MurEmission[] wall;

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
    }
}
