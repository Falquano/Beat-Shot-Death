using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMesure : Mesure
{
    [SerializeField] private GameObject zapLinePrefab;
    [SerializeField] private Transform barrel;

    [SerializeField] LayerMask EnnemyMask;
    // j'ai du rajouter ça, c'est la distance max des pistolets
    [SerializeField] private float range = 100f;


    private void OnEnable()
    {
        //Debug.Log("Ennemy is now shooting");
        tempo.onTimeToShoot.AddListener(Shoot);
        animator.SetBool("Aiming", true);
    }

    private void OnDisable()
    {
        tempo.onTimeToShoot.RemoveListener(Shoot);
        animator.SetBool("Aiming", false);
    }

    private void Shoot()
    {
        if (behavior.Player == null)
            return;

        // Ici il faut ajouter le tir ennemi
        // Fait le au raycast, pas au projectile
        //on calcul la direction entre l'ennemi et le player
        Vector2 ShootToPlayer = behavior.Player.transform.position  - transform.position;

        //on créer un raycast du player dans la direction de la souris de distance max sur un mask sans le player lui-même
        RaycastHit2D RayShootEnnemy = Physics2D.Raycast(transform.position + transform.right * 1.2f, transform.right, range, EnnemyMask);
        //Allez chercher le radius au lieu d'aller chercher une valeur lagique comme ça, faut le faire mais c chiant

        if(RayShootEnnemy.collider != null)
        {
            //print("check");
            ZapLine(barrel.position, RayShootEnnemy.point);

            if(RayShootEnnemy.collider.tag == "Player")
            {
                PlayerHealth ScriptPlayerHealth = RayShootEnnemy.collider.GetComponent<PlayerHealth>();
                ScriptPlayerHealth.DealDamage(50);
                
            }
        }
        else
        {
            //à faire plus tard
        }

        // Lorsque c'est bon, remplace Vector3.zero avec le point de contact du tir
        
    }

    private void ZapLine(Vector3 start, Vector3 end)
    {
        LineRenderer line = Instantiate(zapLinePrefab, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
        line.SetPosition(0, start);
        line.SetPosition(1, end);
    }
}
