using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBarriere : MonoBehaviour
{

    [SerializeField] private Animator animatorBarriere;
    private int numberTouch = 0;

    public void OnTouchedBarriere(/*retourne une normal*/)
    {
        if(numberTouch == 0)
        {
            //barriere jamais touché donc elle se brise une fois
            //check du sens de la normal
            animatorBarriere.SetTrigger("Cotédelanormal");
            numberTouch += 1;
        }
        else if(numberTouch == 1)
        {
            //Barriere deja un peu casser donc on labrise totalement
            animatorBarriere.SetTrigger("Dernierbarriere");
        }
    }

}
