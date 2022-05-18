using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class HitMesure : Mesure
{

    private GameObject Player;
    private MoveMesure ScriptMoveMesure;

    [SerializeField] private float rayOffset = .7f;
    [SerializeField] private int damage = 50;

    [SerializeField] public UnityEvent onHit;

    [SerializeField] private float range = 100f;
    [SerializeField] LayerMask EnnemyMask;

    [SerializeField] GameObject ZoneHit;
    [SerializeField] private float DistanceHit;
    

    private void Start()
    {
        Player = FindObjectOfType<PlayerMove>().gameObject;
        ScriptMoveMesure = GetComponent<MoveMesure>();
    }



    private void OnEnable()
    {
        tempo.onTimeToShoot.AddListener(Hit);
        //animator.SetBool("Aiming", true);

        if (Vector3.Distance(transform.position, Player.transform.position) > DistanceHit)
        {

            ScriptMoveMesure.enabled = true;
        }
    }

    private void OnDisable()
    {
        ZoneHit.SetActive(false);
        tempo.onTimeToShoot.RemoveListener(Hit);
        //animator.SetBool("Aiming", false);

        ScriptMoveMesure.enabled = false;
    }
    public void Hit()
    {
        print(Vector3.Distance(transform.position, Player.transform.position));
        Debug.DrawLine(transform.position, Player.transform.position, Color.red,0.5f);

        //Si l'ennemy mêlé n'est pas proche du player il ne frappe pas
        if (Vector3.Distance(transform.position, Player.transform.position) > DistanceHit)
        {
           
            return;
        }
        ScriptMoveMesure.enabled = false;

        if( PlayerisDead == true)
            return;
        //On invoque l'event pour les anims etc.
        onHit.Invoke();

        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, DistanceHit);
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
