using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShieldGiverScript : MonoBehaviour
{
    [SerializeField] private GameObject thisObject;

    [SerializeField] public UnityEvent onShieldGiven;

   


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Appel de la fonction qui lui donne un shield
            PlayerHealthSystem PlayerHealth = other.GetComponent<PlayerHealthSystem>();
            bool checkShieldIsFull = PlayerHealth.CheckShield();

            if(checkShieldIsFull == false)
            {
                onShieldGiven.Invoke();
                Destroy(thisObject);
            }
        }
    }
}
