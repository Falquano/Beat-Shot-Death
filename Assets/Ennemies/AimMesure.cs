using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimMesure : Mesure
{
    private void OnEnable()
    {
        //Debug.Log("Ennemy is now aiming");
    }

    private void Update()
    {
        // Ici ajouter le code de déplacement. Il faut juste rester tourné vers le joueur et s'approcher de lui
        Transform PlayerTransform = behavior.Player.GetComponent<Transform>();


        float AngleRad = Mathf.Atan2(PlayerTransform.position.y - transform.position.y, PlayerTransform.position.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }
}
