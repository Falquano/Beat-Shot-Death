using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int MaxHealth;
    //[SerializeField] private int MaxShield;

    [SerializeField] public UnityEvent onTakeDamage;
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
        print(health);

        onTakeDamage.Invoke();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        onDie.Invoke();

        Destroy(gameObject);
        // à la place faudrait lancer l'animation de mort et tout
    }
}
