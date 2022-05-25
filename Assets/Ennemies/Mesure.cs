using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnnemyBehavior))]
public class Mesure : MonoBehaviour
{
    protected EnnemyBehavior behavior;
    protected TempoManager tempo;
    //protected Animator animator;

    protected bool PlayerisDead = false;

    [SerializeField] protected float DistanceMaxShoot;
    protected GameObject Player;

    private void Start()
    {
        
    }

    public void Init()
    {
        behavior = GetComponent<EnnemyBehavior>();
        tempo = FindObjectOfType<TempoManager>();
        //animator = GetComponentInChildren<Animator>();
        Player = FindObjectOfType<PlayerMove>().gameObject;
    }
    public void OnDied()
    {
        PlayerisDead = true;
    }

}
