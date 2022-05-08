using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXOndeScript : MonoBehaviour
{
    [SerializeField] private GameObject Onde1;
    [SerializeField] private GameObject Onde2;
    [SerializeField] private GameObject Onde3;
    [SerializeField] private GameObject Onde4;

    //Oubli pas de placer tes vfx sur l'objet ombo et de bien les placer au centre
    //Ensuite tu les déplacer dans l'inspecteur de ce script dans le bon ordre
    //Puis tu met juste la ligne de code qui permet de lire un vfx
    public void OnVFXOndeActive(float combo)
    {
        if(combo >= 0 && combo < 1)
        {
            print("combo à 0");
        }
        else if(combo >= 1 && combo < 10)
        {
            //Play l'onde1
        }
        else if (combo >= 10 && combo < 30)
        {
            //Play onde 2
        }
        else if (combo >= 30 && combo < 60)
        {
            //Play onde 3
        }
        else if (combo >= 60 && combo < 100)
        {
            //Play Onde 4
        }
    }
}
