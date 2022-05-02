using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoNew : MonoBehaviour
{
    // Cette fonction va être appelé quand un tempo commence afin de vérif le combo et en fonction du combo de changer la vitesse de l'anim du cursor et activé la bool
    private int Combo;
    [SerializeField] private Animator tempoAnim;

    public void OnCursorAnim(int combo)
    {
        Combo = combo;

        if (combo >= 0 && combo <= 20)
        {
            //Animdépart.lenght on change sa duration
            //float length1 = tempoA
            //Animphase1.lenght on change sa duration
            //Animphase2.lenght on change sa duration

            tempoAnim.SetTrigger("Begin");

        }
        else if (combo > 20 && combo <= 40)
        {
            //Animdépart.lenght on change sa duration
            //Animphase1.lenght on change sa duration
            //Animphase2.lenght on change sa duration

            tempoAnim.SetTrigger("Begin");
        }
        else if (combo > 40 && combo <= 70)
        {
            //Animdépart.lenght on change sa duration
            //Animphase1.lenght on change sa duration
            //Animphase2.lenght on change sa duration

            tempoAnim.SetTrigger("Begin");
        }
        else if (combo > 70 && combo <= 90)
        {
            //Animdépart.lenght on change sa duration
            //Animphase1.lenght on change sa duration
            //Animphase2.lenght on change sa duration

            tempoAnim.SetTrigger("Begin");
        }
        else
        {
            //Animdépart.lenght on change sa duration
            //Animphase1.lenght on change sa duration
            //Animphase2.lenght on change sa duration

            tempoAnim.SetTrigger("Begin");
        }
    }
}
