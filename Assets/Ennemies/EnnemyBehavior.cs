using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnnemyBehavior : MonoBehaviour
{
    private TempoManager tempo;

    [SerializeField] private Mesure[] mesures;
    private int currentMesure;

    [SerializeField] private bool active;
    public bool Active { get => active; set => active = value; }

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

        for (int i = 0; i < mesures.Length; i++)
        {
            InitMesure(i);
            SetBehaviorEnabled(i, false);
        }

        rigidBodyEnnemy = GetComponent<Rigidbody>();
        
    }

    public void ActivateAtNextMesure()
	{
        active = true;
        activeEnnemies++;
	}

    public void OnNewMesure(int newMesure)
    {
        if (!enabled)
            return;

        if (active)
		{
            SetBehaviorEnabled(currentMesure, false);
            SetBehaviorEnabled(newMesure, true);
		}

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
        active = false;
        activeEnnemies--;
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

    private static uint activeEnnemies = 0;
}
