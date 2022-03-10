using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class HitMesure : MonoBehaviour
{

    [SerializeField] private GameObject Player;

    [SerializeField] private float rayOffset = .7f;
    [SerializeField] private float DistanceMaxForHit = 5f;
    [SerializeField] private int damage = 50;

    [SerializeField] public UnityEvent onHit;

    [SerializeField] private float range = 100f;
    [SerializeField] LayerMask EnnemyMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Hit()
    {
        //Récupération de la position du joueur au début de la mesure
        Vector3 PlayerPos = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
        //L'ennemi regarde le joueur
        transform.LookAt(PlayerPos, Vector3.up);

        //On créer un raycast qui tire devant lui
        Ray ray = new Ray(transform.position + transform.forward * rayOffset, transform.forward);
        Debug.DrawRay(transform.position, ray.direction, Color.white, 0.0f);

        //Si ce raycast touche quelque chose alors on regarde sa distance avec l'objet
        if(Physics.Raycast(ray, out RaycastHit hitInfo, range, EnnemyMask))
        {
            float distanceWithPlayer = Vector3.Distance(transform.position, hitInfo.point);

            //Si l'élément est proche on lui inflige des dégâts
            if (distanceWithPlayer <= DistanceMaxForHit)
            {
                //on récupère son script health (uniquement si c'est le joueur du coup)
                HealthSystem targetHealth = hitInfo.collider.GetComponent<HealthSystem>();
                if (targetHealth != null)
                {
                    //on lui fais des damages
                    targetHealth.DealDamage(damage);
                }
            }

            //Si la distance est supérieure à X (l'élement est trop loin) alors on ne lui inflige rien 

        }

        //On invoque l'event pour les anims etc.
        onHit.Invoke();
        

        
    }
}
