using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationInvoker : MonoBehaviour
{
    [SerializeField] public UnityEvent OnAnimation = new UnityEvent();

    public void Invoke()
    {
        OnAnimation.Invoke();
    }

   
    
}
