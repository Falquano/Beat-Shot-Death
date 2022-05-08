using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
using UnityEditor.VFX;
using UnityEditor.VFX.UI;

public class VFXOndeScript : MonoBehaviour
{
    //Denis: J'ai remplacer GameObject par VisualEffect

    [SerializeField] private VisualEffect Onde1;
    [SerializeField] private VisualEffect Onde2;
    [SerializeField] private VisualEffect Onde3;
    [SerializeField] private VisualEffect Onde4;

    //Condition permettant de faire spawn les ondes une fois sinon 
    //les ondes se joue continuellement  

	public bool ondebool1 = false;
	public bool ondebool2 = false;
	public bool ondebool3 = false;
	public bool ondebool4 = false;


    //Oubli pas de placer tes vfx sur l'objet combo et de bien les placer au centre :Check
    //Ensuite tu les dplacer dans l'inspecteur de ce script dans le bon ordre : Check
    //Puis tu met juste la ligne de code qui permet de lire un vfx : Problme 




    public void OnVFXOndeActive(float combo)
    {
        //Denis: il faut une autre condition (que je peux pas faire vu que je connais pas 100% le code de Victor+ de toi) 
        //qui fait en sorte que quand le combo baisse, l'onde se joue pas car on veut qu'il apparait uniquement lorsqu'il atteint  
        //un certain palier comme else if(combo >= 1 && combo < 10 && !ondebool1 )  par exemple [8/05/2022 11:21]


        if(combo >= 0 && combo < 1)
        {
            
        }
        else if(combo >= 1 && combo < 11 && !ondebool1 ) 
        //modification de 10 par 11 ainsi que le reste dans else if car lorsque le joueur atteint le 1er pallier, l'onde 2 se joue alors que 
        //que normalement l'onde1 est censer etre jouer ainsi de suite 
        {
            //Play l'onde1

            print("combo  1");
            Onde1.Play();


            ondebool1=true; // revient a true pour eviter qu'il joue infiniment
        }
        else if (combo >= 11 && combo < 31 && !ondebool2)
        {
            //Play onde 2
            
            Onde2.Play();
            //ondebool2=true;

        }
        else if (combo >= 31 && combo < 61 && !ondebool3)
        {
            //Play onde 3
            Onde3.Play();
            //ondebool3=true;
 
        }
        else if (combo >= 61 && combo < 100 && !ondebool4)
        {
            //Play Onde 4
            Onde4.Play();
            //ondebool4=true;
        }
        if (combo<100) //Permet d'eviter que l'onde ne se joue jamais car en restant true les ondes se joue plus
        {
            //ondebool1=false;
            //ondebool2=false;
            //ondebool3=false;
            //ondebool4=false;
        }
    }
}
