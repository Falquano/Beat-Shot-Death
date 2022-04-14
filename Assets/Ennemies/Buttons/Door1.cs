using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1 : MonoBehaviour
{
    public int codeDoor = 0;
    [SerializeField] private int NumberCode; 
    

    public void Update()
    {
        if(codeDoor >= NumberCode)
        {
            this.gameObject.SetActive(false);
            
        }
    }
}
