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
        // On est bloqué si il y a un collider dans la zone juste en face du joueur
        //bool blocked = Physics2D.OverlapCircle(transform.position + transform.up * blockedCheckOffset, blockedCheckRadius, blockedCheckMask);
        //animator.SetBool("Blocked", blocked);

        //Get la velocity qui sera utile pour l'anim de déplacement
        Vector3 Velocity = PlayerRB.velocity;
        animator.SetFloat("SpeedX", Velocity.x);
        animator.SetFloat("SpeedZ", Velocity.z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + transform.up * blockedCheckOffset, blockedCheckRadius);
    }

    public void OnShot(ShotInfo shotInfo)
    {
        Debug.Log("Fire");

        if (fireright)
        {
            animator.SetTrigger("Fire");
            fireright = false;
            Debug.Log("TirDroit");
        }
        else
        {
            animator.SetTrigger("FireLeft");
            fireright = true;
            Debug.Log("TirGauche");
        }
        //
        /*if (shotInfo.Quality == ShotQuality.Okay)
            animator.SetTrigger("OkayShot");
        else if (shotInfo.Quality == ShotQuality.Perfect)
            animator.SetTrigger("PerfectShot");
        */
    }




    public void ResetAnimationPlayer() //Appeler à chaque début de temps
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

    //Pas encore activé car je ne sais pas où le désactivé
    public void AnimationDecreaseCombo()
    {
        animator.SetTrigger("DecreaseCombo");
    }
}
