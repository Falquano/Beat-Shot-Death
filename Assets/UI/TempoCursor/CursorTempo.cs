using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTempo : MonoBehaviour
{
    [SerializeField] private Animator Cursor;
    [SerializeField] ShootPlayer scriptshootplayer;

    public void CursorTempoAnim()
    {
        int combo = scriptshootplayer.combo;
        

        if (combo <= 1)
        {
            Cursor.SetTrigger("Combo0");
        }
        else if(combo > 1 && combo <= 10)
        {
            Cursor.SetTrigger("Combo1");
        }
        else if (combo > 10 && combo <= 30)
        {
            Cursor.SetTrigger("Combo2");
        }
        else if (combo > 30 && combo <= 60)
        {
            Cursor.SetTrigger("Combo3");
        }
        else if (combo > 60 )
        {
            Cursor.SetTrigger("Combo4");
        }
    }

    /*private void TempoTrue()
    {
        scriptshootplayer.InTempo(true);
    }

    private void TempoFalse()
    {
        scriptshootplayer.InTempo(false);
    }*/


}
