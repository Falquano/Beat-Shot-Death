using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathPlayer : MonoBehaviour
{

    [SerializeField] List<EnnemyBehavior> Listennemy = new List<EnnemyBehavior>();

    public void DeleteComposantEnnmies()
    {
        Listennemy.AddRange(FindObjectsOfType <EnnemyBehavior> ());

        for (var i = 0; i < Listennemy.Count; i++)
            {
            //Listennemy.RemoveAt(i);
            Listennemy[i].enabled = false;
            
            }


    }


}
