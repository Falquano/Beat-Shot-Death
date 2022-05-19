using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEditor.VFX;
using UnityEditor.VFX.UI;


public class OndeScriptVFX : MonoBehaviour
{
    [SerializeField] private ShootPlayer ScriptShootPlayer;
    private int ComboPlayer;

    [SerializeField] private List<VisualEffect> VFXOndeList = new List<VisualEffect>();

    private void Start()
    {
        ComboPlayer = ScriptShootPlayer.combo;
    }

    public void OnMesureStart()
    {
        //Appelé à chaque début de mesure
        //Calcul le combo et play l'anim de l'onde en fonction

        ComboPlayer = ScriptShootPlayer.combo;
        if (ComboPlayer >1 && ComboPlayer <= 10)
        {
            VFXOndeList[0].Play();
        }
        else if (ComboPlayer > 10 && ComboPlayer <= 30)
        {
            VFXOndeList[1].Play();
        }
        else if (ComboPlayer > 30 && ComboPlayer <= 60)
        {
            VFXOndeList[2].Play();
        }
        else if (ComboPlayer > 60 && ComboPlayer <= 100)
        {
            VFXOndeList[3].Play();
        }
    }
}
