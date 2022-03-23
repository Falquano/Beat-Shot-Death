using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    private HealthSystem Health;

    [SerializeField] private int damage = 50;

  

    private void OnTriggerEnter(Collider other)
    {
        print("ZoneHit");
        Health = other.GetComponent<HealthSystem>();
        Health.DealDamage(damage);
        /*if (other.tag == "Player")
        {
            Health = other.GetComponent<HealthSystem>();
            Health.DealDamage(damage);
        }

        if (other.tag == "Ennemy")
        {
            Health = other.GetComponent<HealthSystem>();
            Health.DealDamage(damage);
        }*/
    }


    
}
