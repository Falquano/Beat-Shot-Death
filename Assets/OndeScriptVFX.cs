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
    public bool Active1=false;
    public bool Active2=false;
    public bool Active3=false;
    public bool Active4=false;

    private void Update()
    {

    }


    public void OnMesureStart()
    {
        //Appel� � chaque d�but de mesure
        //Calcul le combo et play l'anim de l'onde en fonction

        ComboPlayer = ScriptShootPlayer.combo;
        if (ComboPlayer==0)
        {
            Active1=false;
            Active2=false;
            Active3=false;
            Active4=false;
        }
        if (ComboPlayer >1 && ComboPlayer <= 10 && !Active1)
        {
            Active2=false;
            VFXOndeList[0].Play();
            Active1=true;
        }
        else if (ComboPlayer > 11 && ComboPlayer <= 30 && !Active2)
        {
            Active1=false;
            VFXOndeList[1].Play();
            Active2=true;
        }
        else if (ComboPlayer > 31 && ComboPlayer <= 60 && !Active3)
        {
            Active2=false;
            VFXOndeList[2].Play();
            Active3=true;
            Active4=false;
        }
        else if (ComboPlayer > 61 && ComboPlayer <= 100 && !Active4)
        {
            Active3=false;
            VFXOndeList[3].Play();
            Active4=true;
        }
    }

}
