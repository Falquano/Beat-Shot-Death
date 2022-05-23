using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tempo_MurEmission : MonoBehaviour
{
    public Material matStandard;
    public Material matEmissif;
    [SerializeField] private Renderer rend;
    Material m_materiel;
    private Color color;
    private float intensity;
    private float intensityModifier = 100f;
    [SerializeField] private bool pulse;
    public float timer;
    public float pulseTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        pulse = false;
        m_materiel = GetComponent<Renderer>().material;
        rend = GetComponent<Renderer>();
        rend.material = matStandard;
    }

    // Update is called once per frame
    void Update()
    {
        if(pulse)
        {
            timer += Time.deltaTime;
        }

        if (timer >= pulseTime)
        {
            /*intensity = 0;
            matStandard.SetColor("_EmissionColor", color * intensity);
            */
            rend.material = matStandard;
            pulse = false;
            timer = 0;
            Debug.Log("Je suis standard");
        }

    }

    public void OnPulse()
    {
        /*intensity = intensityModifier;
        matStandard.SetColor("_EmissionColor", color * intensity);
        */
        rend.material = matEmissif;
        pulse = true;
        Debug.Log("Je Pulse");
    }
}
