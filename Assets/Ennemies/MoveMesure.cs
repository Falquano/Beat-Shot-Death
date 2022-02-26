using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMesure : Mesure
{
    [SerializeField] private float Distance;
    [SerializeField] private float speed;
    [SerializeField] private float ValueMargin;

    private void Update()
    {
        // Ici ajouter le code de déplacement. Il faut juste rester tourné vers le joueur et s'approcher de lui
        Transform PlayerTransform = behavior.Player.GetComponent<Transform>();


        float AngleRad = Mathf.Atan2(PlayerTransform.position.y - transform.position.y, PlayerTransform.position.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

        //calcul de la direction entre l'ennemi et le player
        Vector2 DirectionPlayer = PlayerTransform.position - transform.position;
        //calcul de la distance entre l'ennemi et le player
        float DistanceWithPlayer = Vector2.Distance(transform.position, PlayerTransform.position);

        if (DistanceWithPlayer < Distance + ValueMargin && DistanceWithPlayer > Distance - ValueMargin)
        {
            print("R");
        }
        else if (DistanceWithPlayer < Distance - ValueMargin)
        {
            behavior.RigidBodyEnnemy.velocity = -DirectionPlayer.normalized * speed;
            print("reculer");
        }
        else if (DistanceWithPlayer > Distance + ValueMargin)
        {
            behavior.RigidBodyEnnemy.velocity = DirectionPlayer.normalized * speed;
            print("avancer");
        }
        
        

    }

    private void OnDisable()
    {
        behavior.RigidBodyEnnemy.velocity = Vector2.zero;
    }
}
