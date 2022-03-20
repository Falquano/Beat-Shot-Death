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

    //Animator de l'objet qui est soit un ennemy soit le player
    Animator thisAnimator;

    private int health;
    public int Health => health;

    private void Start()
    {
        health = MaxHealth;
        thisAnimator = GetComponent<Animator>();
    }
    public void DealDamage(int amount)
    {
        health -= amount;
        

        onTakeDamage.Invoke(amount, health);
        print("touched");

        thisAnimator.SetTrigger("TouchedTrigger");

        if (health <= 0)
        {
            Die();
            thisAnimator.SetTrigger("DeathTrigger");
        }
    }

    private void Die()
    {
        onDie.Invoke();

        Destroy(gameObject);
        // � la place faudrait lancer l'animation de mort et tout
    }
}
