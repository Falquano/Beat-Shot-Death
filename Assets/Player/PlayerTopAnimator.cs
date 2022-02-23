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

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // On est bloqué si il y a un collider dans la zone juste en face du joueur
        bool blocked = Physics2D.OverlapCircle(transform.position + transform.up * blockedCheckOffset, blockedCheckRadius, blockedCheckMask);
        animator.SetBool("Blocked", blocked);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + transform.up * blockedCheckOffset, blockedCheckRadius);
    }

    public void OnShot(ShotInfo shotInfo)
    {
        if (shotInfo.Quality == ShotQuality.Okay)
            animator.SetTrigger("OkayShot");
        else if (shotInfo.Quality == ShotQuality.Perfect)
            animator.SetTrigger("PerfectShot");
    }
}
