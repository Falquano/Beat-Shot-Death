using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : AnimationInvoker
{
    private PlayerMove player;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponentInParent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", player.CurrentSpeed);
    }
}
