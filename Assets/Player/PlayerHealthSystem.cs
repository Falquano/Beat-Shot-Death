using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private int MaxHealth = 3;



    /// <summary>
    /// Event envoyant lors de la prise de d�gat, les d�gats, puis les nouveaux PVs
    /// </summary>
    [SerializeField] public UnityEvent<int, int> onTakeDamage;
    [SerializeField] public UnityEvent onDie;

    //Appel du script d'animation du player
 
    [SerializeField] private List<Component> ListComponentPlayer = new List<Component>();
    

    [Header("Debug")]
    [SerializeField] public bool invincible = false;
    [SerializeField] private bool PlayerIsInvincible = false;
    public GameObject GameOverScreen;
    public GameObject HurtScreen;

    public bool PlayerisDead = false;
    private Rigidbody RB;


    private int health;

    public int Health => health;


    private void Start()
    {
        health = MaxHealth;
        RB = GetComponent<Rigidbody>();

    }
    public void DealDamage(int amount)
    {
        if (invincible || PlayerIsInvincible)
            return;

        if(PlayerisDead == false)
        {
            health -= amount;
            onTakeDamage.Invoke(amount, health);


            if (health <= 0)
            {
                Die();

            }
        }
        
    }

    private void Die()
    {
        for (var i = 0; i < ListComponentPlayer.Count; i++)
        {

            Destroy(ListComponentPlayer[i]);
        }

        onDie.Invoke();
        PlayerisDead = true;
        RB.velocity = Vector3.zero;
        

    }

    public void OnDied()
    {
        GameOverScreen.SetActive(true);
        HurtScreen.SetActive(false);  
    }


}
