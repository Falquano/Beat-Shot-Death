using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerTopAnimator : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float blockedCheckOffset;
    [SerializeField] private float blockedCheckRadius;
    [SerializeField] private LayerMask blockedCheckMask;

    Rigidbody PlayerRB;
    private bool fireright;

    private void Start()
    {
        animator = GetComponent<Animator>();
        PlayerRB = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        

        //Get la velocity qui sera utile pour l'anim de d�placement
        Vector3 Velocity = PlayerRB.velocity;
        Vector3 VelocityRelative = transform.InverseTransformDirection(Velocity);

        VelocityRelative = VelocityRelative.normalized;

        animator.SetFloat("SpeedX", VelocityRelative.x);
        animator.SetFloat("SpeedZ", VelocityRelative.z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + transform.up * blockedCheckOffset, blockedCheckRadius);
    }

    public void OnShot(ShotInfo shotInfo)
    {
        

        if (shotInfo.Quality != ShotQuality.Bad)
        {
            if (fireright)
            {
                animator.SetTrigger("Fire");
                fireright = false;
                //Debug.Log("TirDroit");
            }
            else
            {
                animator.SetTrigger("FireLeft");
                fireright = true;
                //Debug.Log("TirGauche");
            }
        }

     

        
        
    }




    public void ResetAnimationPlayer() //Appeler � chaque d�but de temps
    {
        animator.ResetTrigger("TouchedTrigger");
        animator.ResetTrigger("OkayShot");
        animator.ResetTrigger("PerfectShot");
        
        //thisAnimator.ResetTrigger("DeathTrigger");
    }

    public void AnimationTouchedPlayer()
    {
        animator.SetTrigger("TouchedTrigger");
    }

    public void AnimationDeathPlayer()
    {
        animator.SetTrigger("DeathTrigger");
    }

    //Pas encore activ� car je ne sais pas o� le d�sactiv�
    public void AnimationDecreaseCombo()
    {
        animator.SetTrigger("DecreaseCombo");
    }
}
