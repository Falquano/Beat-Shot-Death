using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonJustShoot : MonoBehaviour
{

    [SerializeField] Door1 ScriptCodeDoor;

    // Start is called before the first frame update
    

    public void DoorOpening()
    {
        ScriptCodeDoor.codeDoor += 1;
    }
}
