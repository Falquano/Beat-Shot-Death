using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    EnnemyBehavior ScriptEnnemy;
    public float myHurting;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) // Aucune collision d�tect�e
    {
        print("triggerOn");
        if(other.gameObject.tag == ("Ennemy"))
        {

            //D�finir dans le script de l'ennemy le nombre de pts de d�g�ts dans une variable, par rapport au tir que l'on  � fait.
            //ScriptEnnemy.HurtingPoint = myHurting;

            //d�truire la balle
            Destroy(this);
        }
    }
}
