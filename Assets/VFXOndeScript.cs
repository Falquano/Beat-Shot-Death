using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXOndeScript : MonoBehaviour
{
    
    public void OnVFXOndeActive(float combo)
    {
        if(combo >= 0 && combo < 1)
        {
            print("caca");
        }
        else if(combo >= 1 && combo < 10)
        {

        }
        else if (combo >= 10 && combo < 30)
        {

        }
        else if (combo >= 30 && combo < 60)
        {

        }
        else if (combo >= 60 && combo < 100)
        {

        }
    }
}
