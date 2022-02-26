using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnnemyBehavior : MonoBehaviour
{
    private TempoManager tempo;
    [SerializeField] private int EnnemyLife = 100;

    [SerializeField] private Mesure[] mesures;
    private int currentMesure;

    [SerializeField] public GameObject Player;

    private Rigidbody2D rigidBodyEnnemy;
    public Rigidbody2D Rigidbody => rigidBodyEnnemy;

    private Animator animator;

    private void Start()
    {
        tempo = FindObjectOfType<TempoManager>();
        tempo.onMesureStart.AddListener(OnNewMesure);
        currentMesure = 0;
        animator = GetComponentInChildren<Animator>();

        for (int i = 1; i < mesures.Length; i++)
        {
            InitMesure(i);
            SetBehaviorEnabled(i, false);
        }
        InitMesure(0);
        SetBehaviorEnabled(0, true);


        rigidBodyEnnemy = GetComponent<Rigidbody2D>();
    }

    public void OnNewMesure(int newMesure)
    {
        if (!enabled)
            return;

        SetBehaviorEnabled(currentMesure, false);
        SetBehaviorEnabled(newMesure, true);
        currentMesure = newMesure;
    }

    private void SetBehaviorEnabled(int index, bool enabled)
    {
        if (mesures[index] != null)
            mesures[index].enabled = enabled;
    }

    private void InitMesure(int index)
    {
        if (mesures[index] != null)
            mesures[index].Init();
    }

    public void DamageEnnemy(int HitLife)
    {
        EnnemyLife -= HitLife;
        animator.SetTrigger("Hurt");

        if(EnnemyLife <= 0)
        {
            //Destroy(this.gameObject);
            enabled = false;
            SetBehaviorEnabled(currentMesure, false);
            animator.SetBool("Alive", false);

            Die();
        }
    }

    private void Die()
    {
        foreach (Mesure mesure in mesures)
            Destroy(mesure);
        Destroy(Rigidbody);
        Destroy(GetComponent<Collider2D>());
        Destroy(this);
    }
}
