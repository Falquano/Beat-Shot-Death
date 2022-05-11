using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonJustShoot : MonoBehaviour
{



    [SerializeField] private Door[] doors;
    [SerializeField] private bool close;
    public List<SpriteRenderer> colorMat = new List<SpriteRenderer>();
    private bool detruit;


    private void Start()
    {

        //doors = GetComponentsInChildren<Door>();
        close = false;
        detruit = false;
    }

    // Start is called before the first frame update

        
    public void DoorOpening()
    {
        if (detruit)
            return;

        else
            close = true;
            detruit = true;
            DoorClose();

    }

    private void DoorClose()
    {
        if (!close)
            return;

        if((close) && (detruit))
        {
            foreach (Door door in doors)
            {
                door.Close();
            }

            for (var i = 0; i < colorMat.Count; i++)
            {
                colorMat[i].color = Color.red;
            }

        }

    }

}
