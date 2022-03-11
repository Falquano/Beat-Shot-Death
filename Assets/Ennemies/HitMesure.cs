using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class HitMesure : Mesure
{

    [SerializeField] private GameObject Player;

    [SerializeField] private float rayOffset = .7f;
    [SerializeField] private float DistanceMaxForHit = 5f;
    [SerializeField] private int damage = 50;

    [SerializeField] public UnityEvent onHit;

    [SerializeField] private float range = 100f;
    [SerializeField] LayerMask EnnemyMask;

   


    private void OnEnable()
    {
        
        tempo.onTimeToShoot.AddListener(Hit);
        //animator.SetBool("Aiming", true);
    }

    private void OnDisable()
    {
        tempo.onTimeToShoot.RemoveListener(Hit);
        //animator.SetBool("Aiming", false);
    }
    private void Hit()
    {
        

        

        //On invoque l'event pour les anims etc.
        onHit.Invoke();
        

        
    }
}
