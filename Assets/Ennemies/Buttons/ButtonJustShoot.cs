using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonJustShoot : MonoBehaviour
{



    [SerializeField] private Door[] doors;
    [SerializeField] private bool close;
    public List<SpriteRenderer> colorMat = new List<SpriteRenderer>();
    public List<GameObject> target = new List<GameObject>();
    private bool detruit;
    [SerializeField] private StudioEventEmitter destroyEmitter;
    private EnnemyWaves letsCloseSomeDoors;
    [SerializeField] Transform spawnerPoint;

    private void Start()
    {

        //doors = GetComponentsInChildren<Door>();
        close = false;
        detruit = false;
        letsCloseSomeDoors = GetComponentInParent<EnnemyWaves>();
    }

    // Start is called before the first frame update

        
    public void DoorOpening()
    {
        if (detruit)
            return;

        close = true;
        detruit = true;
        destroyEmitter.Play();
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

            letsCloseSomeDoors.DisposeSpawn(spawnerPoint);

            for (var j = 0; j < target.Count; j++)
            {
                target[j].SetActive(false);
            }


            for (var i = 0; i < colorMat.Count; i++)
            {
                colorMat[i].color = Color.red;
            }


        }

    }

}
