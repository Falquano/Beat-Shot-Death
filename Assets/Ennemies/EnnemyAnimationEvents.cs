using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnnemyAnimationEvents : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    Rigidbody EnnemyRB;

    [SerializeField] public UnityEvent onFootstep;

    private void Start()
    {
        animator = GetComponent<Animator>();
        EnnemyRB = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Get la velocity qui sera utile pour l'anim de déplacement
        Vector3 Velocity = agent.velocity;
        Vector3 VelocityRelative = transform.InverseTransformDirection(Velocity).normalized;

        animator.SetFloat("SpeedX", VelocityRelative.x);
        animator.SetFloat("SpeedZ", VelocityRelative.z);
    }


    public void OnHit()
    {
        if(gameObject.name == "EnnemyMelee")
        {
            animator.SetInteger("AttackRand", Random.Range(1, 4));
            animator.SetTrigger("OnHit");
            
        }

        if (gameObject.name == "EnnemyShot")
        {
            animator.SetTrigger("OnHit");
        }

    }

    

    public void OnShoot()
    {
        animator.SetTrigger("OnShoot");
    }

    public void OnCharge()//Oui je ne sais pas comment on dis charger en anglais car je pense que load n'est pas approprié
    {
        animator.SetTrigger("OnShoot");
    }

    /*public void Footstep() Je sais pas pourquoi ya ça
     
    {
        onFootstep.Invoke();
    }*/


}
