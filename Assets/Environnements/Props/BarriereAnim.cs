using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarriereAnim : MonoBehaviour
{
    [SerializeField] private GameObject barriere;
    [SerializeField] private GameObject barriere1_front;
    [SerializeField] private GameObject barriere2_front;
    [SerializeField] private GameObject barriere1_back;
    [SerializeField] private GameObject barriere2_back;
    [SerializeField] private GameObject collider1;
    [SerializeField] private GameObject collider2;
    private Animation anim1_front;
    private Animation anim2_front;
    private Animation anim1_back;
    private Animation anim2_back;
    private bool step1;
    private bool step2;

    private int numberShot= 0;


    // Start is called before the first frame update
    void Start()
    {
        anim1_front = barriere1_front.GetComponent<Animation>();
        anim2_front = barriere2_front.GetComponent<Animation>();
        anim1_back = barriere1_back.GetComponent<Animation>();
        anim2_back = barriere2_back.GetComponent<Animation>();
        step2 = false;
        step1 = true;
    }

    public void shotCollision()//Tu appel ça depuis le script du player ou d'un ennemi si il tir et que c'est cette objet +++ Sans oublié de lui donné le raycast du tir
    {
        if(numberShot == 0)
        {
            //récupération de la normal du raycast
            numberShot += 1;
        }
        else
        {

        }
    }

    public void hitCollision()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        /*if(collision.gameObject.tag == "Player" && step1 == true)
        {
            if(//Collision avec en front)
            {
                barriere.SetActive(false);
                barriere1_front.SetActive(true);
                barriere2_front.SetActive(false);
                barriere1_back.SetActive(true);
                barriere2_back.SetActive(false);
                anim1_front.Play("Scene");
            }

            if (//Collision avec en back)
            {
                barriere.SetActive(false);
                barriere1_front.SetActive(true);
                barriere2_front.SetActive(false);
                barriere1_back.SetActive(true);
                barriere2_back.SetActive(false);
                anim1_back.Play("Scene");
            }
            //print("je suis collision");

            step1 = false;
            step2 = true;
        }

        /*if (collision.gameObject.tag == "Player" && step2 == true)
        {
            if(Step 2 front)
            {
                 barriere.SetActive(false);
                barriere1_front.SetActive(false);
                barriere2_front.SetActive(true);
                barriere1_back.SetActive(false);
                barriere2_back.SetActive(true);
                anim2_front.Play("Scene");
            }

            if(Step 2 back)
            {
                barriere.SetActive(false);
                barriere1_front.SetActive(false);
                barriere2_front.SetActive(true);
                barriere1_back.SetActive(false);
                barriere2_back.SetActive(true);
                anim2_back.Play("Scene");
            }

        }
        */

    }

}
