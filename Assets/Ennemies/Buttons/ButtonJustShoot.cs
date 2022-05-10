using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonJustShoot : MonoBehaviour
{



    [SerializeField] private Door[] doors;
    [SerializeField] private bool close;
    public List<SpriteRenderer> colorMat = new List<SpriteRenderer>();


    private void Start()
    {

        //doors = GetComponentsInChildren<Door>();
        close = false;
    }

    // Start is called before the first frame update

        
    public void DoorOpening()
    {
        close = true;
        DoorClose();

    }

    private void DoorClose()
    {
        if (!close)
            return;

        close = true;
        foreach (Door door in doors)
        {
            Debug.Log("jevaisMarcherTKT");
            door.Close();
        }

        for (var i = 0; i < colorMat.Count; i++)
        {
            colorMat[i].color = Color.red; 
        }

    }

}
