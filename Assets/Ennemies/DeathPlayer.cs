using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathPlayer : MonoBehaviour
{


    [SerializeField] private List<AimMesure> listaim = new List<AimMesure>();
    [SerializeField] private List<MoveMesure> listmove = new List<MoveMesure>();
    [SerializeField] private List<ShootMesure> listshoot = new List<ShootMesure>();
    [SerializeField] private List<EnnemyBehavior> listbehavior = new List<EnnemyBehavior>();
    [SerializeField] private List<Rigidbody> ListRigidbody = new List<Rigidbody>();

  

    public void DeleteComposantEnnmies()
    {
        listbehavior.AddRange(FindObjectsOfType <EnnemyBehavior> ());
        listaim.AddRange(FindObjectsOfType<AimMesure>());
        listmove.AddRange(FindObjectsOfType<MoveMesure>());
        listshoot.AddRange(FindObjectsOfType<ShootMesure>());
        ListRigidbody.AddRange(FindObjectsOfType<Rigidbody>());


        for (var i = 0; i < listbehavior.Count; i++)
        {
            listbehavior[i].enabled = false;
            listaim[i].enabled = false;
            listmove[i].enabled = false;
            listshoot[i].enabled = false;

            ListRigidbody[i].velocity = Vector3.zero;


        }
    }


}
