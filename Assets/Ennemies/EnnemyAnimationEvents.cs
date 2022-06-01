using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.VFX;


public class EnnemyAnimationEvents : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    Rigidbody EnnemyRB;

    [SerializeField] public UnityEvent onFootstep;
    [SerializeField] private bool melee;


    private void Start()
    {
        animator = GetComponent<Animator>();
        EnnemyRB = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        //Get la velocity qui sera utile pour l'anim de d�placement
        Vector3 Velocity = agent.velocity;
        Vector3 VelocityRelative = transform.InverseTransformDirection(Velocity).normalized;

        animator.SetFloat("SpeedX", VelocityRelative.x);
        animator.SetFloat("SpeedZ", VelocityRelative.z);

    }


    public void OnHit()
    {
        if (melee)
        {
            animator.SetInteger("AttackRand", Random.Range(1, 4));
        }
         animator.SetTrigger("OnHit");
    }

    

    public void OnShoot()
    {
        animator.SetTrigger("OnShoot");
        
    }

    public void OnCharge()//Oui je ne sais pas comment on dis charger en anglais car je pense que load n'est pas appropri�
    {
        
        animator.SetTrigger("OnShoot");
    }

    public void OnAim() //Uniquement sur les tourelle
    {
           
    }
    


    public void Footstep() //Je sais pas pourquoi ya �a
    {
        onFootstep.Invoke();
    }


}
