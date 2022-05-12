using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTempo : MonoBehaviour
{
    /*[SerializeField] private Animator Cursor;
    [SerializeField] ShootPlayer scriptshootplayer;*/

    [SerializeField] GameObject PrefabUI;

    [SerializeField]  private List<GameObject> AnimList = new List<GameObject>();

    //Pour chaque début de beat on créer une anim qui est stocker dans une list
    public void NewUIOnBeat()
    {
        GameObject myAnim = Instantiate(PrefabUI, transform);
        AnimList.Add(myAnim);

    }


    //Pour chaque début de beat, on active le trigger de toutes les anim qui existe
    public void TriggerAnimSet()
    {
        print("proutasse");
        for (var i = 0; i < AnimList.Count; i++)
        {
            Animator AnimAnimator = AnimList[i].GetComponent<Animator>();
            AnimAnimator.SetTrigger("Trigger");
        }
    }

    //Quand un anim se détruit, on la supprime du tableau
    public void OnAnimDestroy(GameObject anim)
    {
        AnimList.Remove(anim);
    }














    /*public void CursorTempoAnim()
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
    }*/

    /*private void TempoTrue()
    {
        scriptshootplayer.InTempo(true);
    }

    private void TempoFalse()
    {
        scriptshootplayer.InTempo(false);
    }*/


}
