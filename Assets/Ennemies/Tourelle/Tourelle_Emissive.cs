using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Tourelle_Emissive : MonoBehaviour
{

    [SerializeField] private Animator animatorTourelle;
    [SerializeField] private Animator anneau;
    [SerializeField] private GameObject entier;
    [SerializeField] private GameObject destroy;
    [SerializeField] private Material emissiveMaterial;
    [SerializeField] private Renderer objecToChange;
    
    private Color color;
    public bool intensityOverTime;


    private void Start()
    {
        animatorTourelle = GetComponent<Animator>();
        anneau = GetComponent<Animator>();
        emissiveMaterial = objecToChange.GetComponent<Renderer>().material;
    }

    private void Update()
    {

        /*if (intensityOverTime)
        {
            timer += Time.deltaTime;
            Debug.Log("Timer =" + timer);
        }

        emissiveMaterial.SetColor("_Emission", color * timer);*/
    }


    public void OnShoot()
    {
        animatorTourelle.SetTrigger("OnShoot");
        intensityOverTime = false;

    }

    public void OnAim() //Uniquement sur les tourelle
    {
        intensityOverTime = true;
        emissiveMaterial.EnableKeyword("_EMISSION");
        emissiveMaterial.SetColor("_EmissionColor", color * 10);
        print("okkkkkkkk");


    }

    public void OnDeath()
    {
        entier.SetActive(false);
        destroy.SetActive(true);
        print("Destruction!");
        anneau.SetTrigger("OnDeath");
    }


}
