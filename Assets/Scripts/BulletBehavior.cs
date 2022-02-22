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

    private void OnTriggerEnter(Collider other) // Aucune collision détectée
    {
        print("triggerOn");
        if(other.gameObject.tag == ("Ennemy"))
        {

            //Définir dans le script de l'ennemy le nombre de pts de dégâts dans une variable, par rapport au tir que l'on  à fait.
            //ScriptEnnemy.HurtingPoint = myHurting;

            //détruire la balle
            Destroy(this);
        }
    }
}
