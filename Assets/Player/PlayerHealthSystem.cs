using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private int MaxHealth = 5;

    [SerializeField] private int MaxShield = 5;

    /// <summary>
    /// Event envoyant lors de la prise de dégat, les dégats, puis les nouveaux PVs
    /// </summary>
    [SerializeField] public UnityEvent<int, int, int> onTakeDamage;
    [SerializeField] public UnityEvent onDie;

    //Appel du script d'animation du player
    [SerializeField] private AnimationInvoker ScriptAnimation;

    //UI du shield

    [SerializeField] private List<GameObject> ShieldUI = new List<GameObject>();
    [SerializeField] private List<GameObject> LifeUI = new List<GameObject>();

    [Header("Debug")]
    [SerializeField] private bool invincible = false;


    private int health;
    private int shield;
    public int Health => health;
    public int Shield => shield;

    private void Start()
    {
        health = MaxHealth;
        shield = MaxShield;
        ShieldFonctionUI();
        LifeFonctionUI();

    }
    public void DealDamage(int amount)
    {
        if (invincible)
            return;

        //Verification qu'il reste des points de shield
        if(shield > 0)
        {
            shield -= amount;
            if(shield < 0)
            {
                health += shield;
                shield = 0;
            }
        }
        else //Si il n'y a plus de pts de shield, alors on retire de la vie
        {
            health -= amount;
        }


        onTakeDamage.Invoke(amount, health, shield);

        //Mise à jour de l'UI
        ShieldFonctionUI();
        LifeFonctionUI();


        if (health <= 0)
        {
            Die();

        }
    }

    private void Die()
    {
        onDie.Invoke();

        Destroy(gameObject);
        
    }

    public bool CheckShield()
    {
        bool shieldIsFull;
        if(shield < MaxShield)
        {
            shieldIsFull = false;
            return shieldIsFull;
        }
        else
        {
            shieldIsFull = true;
            return shieldIsFull;
        }
        
    }
    public void OnShieldGive()
    {

        if(shield < MaxShield)
        {
            //Ajout d'un pts de shield et mise à jour de l'UI
            shield += 1;
            ShieldFonctionUI();
        }
        
    }


    public void ShieldFonctionUI()
    {
        //Désactivation de tout les UI  de shield
        for (int i = 0; i < MaxShield; i++)
        {

            GameObject UIShieldObject = ShieldUI[i];
            UIShieldObject.SetActive(false);
        }


        //Activation de l'UI en fonction du nombre de shield restant
        for (int i = 0; i < shield; i++)
        {

            GameObject UIShieldObject = ShieldUI[i];
            UIShieldObject.SetActive(true);
        }

        
    }
    public void LifeFonctionUI()
    {
        //Désactivation de tout les UI  de shield
        for (int i = 0; i < MaxHealth; i++)
        {

            GameObject UILifeObject = LifeUI[i];
            UILifeObject.SetActive(false);
        }


        //Activation de l'UI en fonction du nombre de shield restant
        for (int i = 0; i < health; i++)
        {

            GameObject UILifeObject = LifeUI[i];
            UILifeObject.SetActive(true);
        }


    }
}
