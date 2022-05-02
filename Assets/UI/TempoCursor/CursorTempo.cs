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
        else if(combo > 1 && combo <= 25)
        {
            Cursor.SetTrigger("Combo1");
        }
        else if (combo > 25 && combo <= 50)
        {
            Cursor.SetTrigger("Combo2");
        }
        else if (combo > 50 && combo <= 75)
        {
            Cursor.SetTrigger("Combo3");
        }
        else if (combo > 75 )
        {
            Cursor.SetTrigger("Combo4");
        }
    }


}
