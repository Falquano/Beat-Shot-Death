using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboUI : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeCombo(int combo, int max)
    {

        if (combo >= 0 && combo <= 20)
        {
            animator.SetInteger("ComboInt", 0);
        }
        else if (combo > 20 && combo <= 40)
        {
            animator.SetInteger("ComboInt", 1);
        }
        else if (combo > 40 && combo <= 60)
        {
            animator.SetInteger("ComboInt", 2);
        }
        else if (combo > 60 && combo <= 80)
        {
            animator.SetInteger("ComboInt", 3);
        }
        else if (combo > 80 && combo <= 100)
        {
            animator.SetInteger("ComboInt", 4);
        }
    }
}
