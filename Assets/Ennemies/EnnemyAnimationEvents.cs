using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnnemyAnimationEvents : MonoBehaviour
{

    private Animator animator;
    Rigidbody EnnemyRB;

    [SerializeField] public UnityEvent onFootstep;

    private void Start()
    {
        animator = GetComponent<Animator>();
        EnnemyRB = GetComponent<Rigidbody>();
    }
    private void Update()
    {
       
        //Get la velocity qui sera utile pour l'anim de déplacement
        Vector3 Velocity = EnnemyRB.velocity;
        Vector3 VelocityRelative = transform.InverseTransformDirection(Velocity);

        animator.SetFloat("SpeedX", VelocityRelative.x);
        animator.SetFloat("SpeedZ", VelocityRelative.z);
    }


    public void OnHit()
    {
        animator.SetTrigger("OnHit");
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
