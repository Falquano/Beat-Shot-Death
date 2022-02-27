using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnnemyAnimationEvents : MonoBehaviour
{
    [SerializeField] public UnityEvent onFootstep;

    public void Footstep()
    {
        onFootstep.Invoke();
    }
}
