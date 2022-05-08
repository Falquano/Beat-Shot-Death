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
        //print(combo);

        if (combo >= 0 && combo <= 1)
        {
            animator.SetInteger("ComboInt", 0);
        }
        else if (combo > 1 && combo <= 10)
        {
            animator.SetInteger("ComboInt", 1);
        }
        else if (combo > 10 && combo <= 30)
        {
            animator.SetInteger("ComboInt", 2);
        }
        else if (combo > 30 && combo <= 60)
        {
            animator.SetInteger("ComboInt", 3);
        }
        else if (combo > 60 && combo <= 100)
        {
            animator.SetInteger("ComboInt", 4);
        }
    }
}
