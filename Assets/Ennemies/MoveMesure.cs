using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMesure : Mesure
{
    [SerializeField] private float Distance;
    [SerializeField] private float speed;
    [SerializeField] private float ValueMargin;

    private void OnEnable()
    {
        animator.SetBool("Moving", true);
        animator.SetBool("Aiming", false);
    }

    private void OnDisable()
    {
        animator.SetBool("Moving", false);
        behavior.Rigidbody.velocity = Vector3.zero;
    }

    private void Update()
    {
        if (behavior.Player == null)
            return;
        
        // Ici ajouter le code de déplacement. Il faut juste rester tourné vers le joueur et s'approcher de lui
        Transform playerTransform = behavior.Player.GetComponent<Transform>();


        /*float AngleRad = Mathf.Atan2(PlayerTransform.position.z - transform.position.z, PlayerTransform.position.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, AngleDeg, 0);*/
        Vector3 direction = playerTransform.position - transform.position;
        direction.y = 0;
        transform.right = direction.normalized;

        //calcul de la distance entre l'ennemi et le player
        float DistanceWithPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (DistanceWithPlayer < Distance + ValueMargin && DistanceWithPlayer > Distance - ValueMargin)
        {
            //print("R1");
        }
        else if (DistanceWithPlayer < Distance - ValueMargin)
        {
            behavior.Rigidbody.velocity = -direction.normalized * speed;
            //print("reculer");
        }
        else if (DistanceWithPlayer > Distance + ValueMargin)
        {
            behavior.Rigidbody.velocity = direction.normalized * speed;
            //print("avancer");
        }
    }
}
