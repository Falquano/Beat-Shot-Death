using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int MaxHealth = 100;

    //[SerializeField] private int MaxShield;

    /// <summary>
    /// Event envoyant lors de la prise de dégat, les dégats, puis les nouveaux PVs
    /// </summary>
    [SerializeField] public UnityEvent<int, int> onTakeDamage;
    [SerializeField] public UnityEvent onDie;

    //Appel du script d'animation du player
    [SerializeField] private AnimationInvoker ScriptAnimation;

    [Header("Debug")]
    [SerializeField] private bool invincible = false;
    


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


        // à la place faudrait lancer l'animation de mort et tout
    }
}
