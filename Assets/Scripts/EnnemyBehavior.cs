using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnnemyBehavior : MonoBehaviour
{
    private TempoManager tempo;

    [SerializeField] private Mesure[] mesures;
    private int currentMesure;

    private GameObject player;
    public GameObject Player => player;

    private Rigidbody rigidBodyEnnemy;
    public Rigidbody Rigidbody => rigidBodyEnnemy;

    private Animator animator;

    private void Start()
    {
        player = FindObjectOfType<PlayerMove>().gameObject;
        tempo = FindObjectOfType<TempoManager>();
        tempo.onMesureStart.AddListener(OnNewMesure);
        currentMesure = 0;
        animator = GetComponentInChildren<Animator>();
        animator.SetFloat("Seed", Random.Range(0f, 1f));

        for (int i = 1; i < mesures.Length; i++)
        {
            InitMesure(i);
            SetBehaviorEnabled(i, false);
        }
        InitMesure(0);
        SetBehaviorEnabled(0, true);


        rigidBodyEnnemy = GetComponent<Rigidbody>();
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

    public void OnDamage(int hitLife, int newLife)
    {
        animator.SetTrigger("Hurt");
    }

    public void Die()
    {
        //Destroy(this.gameObject);
        enabled = false;
        SetBehaviorEnabled(currentMesure, false);
        animator.SetBool("Alive", false);
        foreach (Mesure mesure in mesures)
            Destroy(mesure);
        Destroy(Rigidbody);
        Destroy(GetComponent<Collider2D>());
        Destroy(this);
    }
}
