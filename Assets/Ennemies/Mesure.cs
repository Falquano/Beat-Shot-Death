using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnnemyBehavior))]
public class Mesure : MonoBehaviour
{
    protected EnnemyBehavior behavior;
    protected TempoManager tempo;
    protected Animator animator;

    public void Init()
    {
        behavior = GetComponent<EnnemyBehavior>();
        tempo = FindObjectOfType<TempoManager>();
        animator = GetComponentInChildren<Animator>();
    }
}
