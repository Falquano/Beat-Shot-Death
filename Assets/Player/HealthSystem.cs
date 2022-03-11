using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int MaxHealth = 100;
    //[SerializeField] private int MaxShield;

    /// <summary>
    /// Event envoyant lors de la prise de d�gat, les d�gats, puis les nouveaux PVs
    /// </summary>
    [SerializeField] public UnityEvent<int, int> onTakeDamage;
    [SerializeField] public UnityEvent onDie;

    private int health;
    public int Health => health;

    private void Start()
    {
        health = MaxHealth;
    }
    public void DealDamage(int amount)
    {
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

        Destroy(gameObject);
        // � la place faudrait lancer l'animation de mort et tout
    }
}
