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
            onTakeDamage.Invoke(amount, health);//Deux visu diff en fonction des pts de vie qui reste genre 1 coups un peu rouge / 2 coups bcp rouge / 3 coup mort



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
        //D�sactiver les ennemis pour qu'ils arr�tent de victimiser le cadavre du player

    }

    public void OnDied()
    {
        print("mort");
        PlayerisDead = false;
<<<<<<< HEAD
        //SceneManager.LoadScene("Hall_2", LoadSceneMode.Single);
        GameOverScreen.SetActive(true);
        HurtScreen.SetActive(false); //hurt effect disparait 
=======
        SceneManager.LoadScene("DeathScene", LoadSceneMode.Single);
        
        //GameOverScreen.SetActive(true);
        //HurtScreen.SetActive(false);
>>>>>>> f4440c1d2e92623f81b4591c96373c97a7c239e1
        

    }


}
