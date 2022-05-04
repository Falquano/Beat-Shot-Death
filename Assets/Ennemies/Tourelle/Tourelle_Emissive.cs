using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Tourelle_Emissive : MonoBehaviour
{

    [SerializeField] private Animator animatorTourelle;
    [SerializeField] private GameObject entier;
    [SerializeField] private GameObject anneau;
    [SerializeField] private GameObject destroy;
    [SerializeField] private GameObject Light;

    [SerializeField] private Animation anim;



    private void Start()
    {
        anim = Light.GetComponent<Animation>();
        animatorTourelle = GetComponent<Animator>();
        anim.Play("Idle-Light");

    }

    private void Update()
    {

    }


    public void OnShoot()
    {
        animatorTourelle.SetTrigger("OnShoot");

        anim.Play("Idle-Light");

    }

    public void OnAim()
    {

        print("okkkkkkkk");

        anim.Play("FeedBack_Light");


    }

    public void OnDeath()
    {
        entier.SetActive(false);
        destroy.SetActive(true);
        anneau.SetActive(false);
        animatorTourelle.SetTrigger("OnDeath");
    }

    


}
