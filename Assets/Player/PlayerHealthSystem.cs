using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private int MaxHealth = 3;



    /// <summary>
    /// Event envoyant lors de la prise de dégat, les dégats, puis les nouveaux PVs
    /// </summary>
    [SerializeField] public UnityEvent<int, int> onTakeDamage;
    [SerializeField] public UnityEvent onDie;

    //Appel du script d'animation du player
 
    [SerializeField] private List<Component> ListComponentPlayer = new List<Component>();
    

    [Header("Debug")]
    [SerializeField] public bool invincible = false;

    public bool PlayerisDead = false;


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
        //Désactiver les ennemis pour qu'ils arrêtent de victimiser le cadavre du player

    }

    public void OnDied()
    {
        print("prpout");
        PlayerisDead = false;
        SceneManager.LoadScene("Hall_2", LoadSceneMode.Single);

    }


}
