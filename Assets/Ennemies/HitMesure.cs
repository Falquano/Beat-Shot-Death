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

    [SerializeField] GameObject ZoneHit;

    


    private void OnEnable()
    {
        tempo.onTimeToShoot.AddListener(Hit);
        //animator.SetBool("Aiming", true);
    }

    private void OnDisable()
    {
        ZoneHit.SetActive(false);
        tempo.onTimeToShoot.RemoveListener(Hit);
        //animator.SetBool("Aiming", false);
    }
    public void Hit()
    {
        if( PlayerisDead == true)
            return;
        //On invoque l'event pour les anims etc.
        onHit.Invoke();

        
    }

    public void AnimOnHit()
    {
        //On active un objet qui sera la zone que l'ennemi touche avec son hit
        ZoneHit.SetActive(true);
    }

    public void AnimEndHit()
    {
        ZoneHit.SetActive(false);
    }


    
}
