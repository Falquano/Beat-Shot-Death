using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBeat : MonoBehaviour
{
    public void Tick()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.forward, Color.red, 0.1f);
    }
}
