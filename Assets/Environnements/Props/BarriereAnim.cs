using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarriereAnim : MonoBehaviour
{
    [SerializeField] private GameObject barriere;
    [SerializeField] private GameObject barriere1;
    [SerializeField] private GameObject barriere2;
    [SerializeField] private GameObject collider1;
    [SerializeField] private GameObject collider2;
    private Animation anim;
    private Animation anim1;
    private bool step1;
    private bool step2;


    // Start is called before the first frame update
    void Start()
    {
        anim = barriere1.GetComponent<Animation>();
        anim1 = barriere2.GetComponent<Animation>();
        step2 = false;
        step1 = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown("Space"))
        {
            barriere.SetActive(false);
            barriere1.SetActive(true);
        }

        if (Input.GetKeyDown("Fire1"))
        {
            barriere1.SetActive(false);
            barriere2.SetActive(true);
            collider1.SetActive(false);
            collider2.SetActive(false);
        }
        */
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && step1 == true)
        {
            barriere.SetActive(false);
            barriere1.SetActive(true);
            print("je suis collisioné");
            anim.Play("Scene");
            step1 = false;
            step2 = true;
        }

        if (collision.gameObject.tag == "Player" && step2 == true)
        {
            barriere1.SetActive(false);
            barriere2.SetActive(true);
            print("je suis collisioné");
            anim1.Play("Scene");
            step2 = false;
        }

    }

}
