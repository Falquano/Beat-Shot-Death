using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCursorOnCursor : MonoBehaviour
{
    [SerializeField] private CursorTempo ScriptGestionAnim;
    [SerializeField] private GameObject thisObject;

    public void OnAnimFinished()
    {
        ScriptGestionAnim.OnAnimDestroy(thisObject);
        Destroy(thisObject);
    }
}
