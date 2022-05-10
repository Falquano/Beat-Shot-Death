using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    private HealthSystem Health;
    private PlayerHealthSystem PlayerHealth;

    [SerializeField] private int damageEnnemy = 50;
    [SerializeField] private int damagePlayer = 2;



    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            
            PlayerHealth = other.GetComponent<PlayerHealthSystem>();
            PlayerHealth.DealDamage(damagePlayer);
        }
        
        if(other.tag == "Ennemy")
        {
            
            Health = other.GetComponent<HealthSystem>();
            Health.DealDamage(damageEnnemy);
        }

        
        
    }


    
}
