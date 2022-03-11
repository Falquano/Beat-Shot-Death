using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    HealthSystem EnnemiHealth;

    [SerializeField] private int damage = 50;

    // Start is called before the first frame update
    void Start()
    {
        //R�cup�ration du script health de l'ennemy
        EnnemiHealth = GetComponent<HealthSystem>();

    }

   

    private void OnCollisionEnter(Collision collision)
    {
        //Si il y as collision, le script health de l'objet touch� est r�cup�r�
        HealthSystem TouchedElementHealth = collision.collider.GetComponent<HealthSystem>();

        //Si le script health n'est pas celui de l'ennemy lui m�me ou qu'il n'est pas nul (donc ce n'est pas le player ou un ennemi)
        if(TouchedElementHealth != EnnemiHealth && TouchedElementHealth!= null)
        {
            TouchedElementHealth.DealDamage(damage);
        }
        
    }
}
