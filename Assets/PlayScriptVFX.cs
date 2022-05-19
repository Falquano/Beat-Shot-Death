using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEditor.VFX;
using UnityEditor.VFX.UI;


public class PlayScriptVFX : MonoBehaviour
{
    private VisualEffect Onde;

    private void Start()
    {
        Onde = GetComponent<VisualEffect>();
    }

    public void PlayVFX()
    {
        Onde.Play();
    }
}
