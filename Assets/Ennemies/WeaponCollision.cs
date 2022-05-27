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
        else if (other.tag == "Barriere")
        {
            
            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(transform.position, transform.forward, Color.green, 0.5f);

            //faire un raycast pour shopper la normal
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                
                if (hitInfo.collider.tag == "Barriere")
                {
                    BarriereAnim barriere = hitInfo.transform.GetComponent<BarriereAnim>();
                    barriere.ShotCollision(hitInfo.normal);
                }
            }
                
        }



    }


    
}
