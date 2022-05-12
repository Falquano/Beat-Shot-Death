using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCursorOnCursor : MonoBehaviour
{
    [SerializeField] private Animator Anim;
    

    public void OnBeatBegin()
    {
        Anim.SetTrigger("Trigger");
    }
}
