using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int MaxHealth = 100;

    //[SerializeField] private int MaxShield;

    /// <summary>
    /// Event envoyant lors de la prise de d�gat, les d�gats, puis les nouveaux PVs
    /// </summary>
    [SerializeField] public UnityEvent<int, int> onTakeDamage;
    [SerializeField] public UnityEvent onDie;

    //Appel du script d'animation du player
    [SerializeField] private AnimationInvoker ScriptAnimation;

    [Header("Debug")]
    [SerializeField] private bool invincible = false;
    public GameObject damageText;
    


    private int health;
    public int Health => health;

    private void Start()
    {
        health = MaxHealth;
    }

    public void DealDamage(int amount)
    {
        if (invincible)
            return;

        health -= amount;
        
        
        onTakeDamage.Invoke(amount, health); 
        DamageIndicator indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.SetDamageText(amount);

        ///

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        onDie.Invoke();
        
        
        Destroy(GetComponent<ShootMesure>());
        Destroy(GetComponent<AimMesure>());
        Destroy(GetComponent<EnnemyBehavior>());


        // � la place faudrait lancer l'animation de mort et tout
    }
}
