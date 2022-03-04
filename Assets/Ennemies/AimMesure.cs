using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimMesure : Mesure
{
    public float rotationSpeed;

    private void OnEnable()
    {
        //Debug.Log("Ennemy is now aiming");
        animator.SetBool("Aiming", true);
    }

    private void OnDisable()
    {
        //animator.SetBool("Aiming", false);
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


        //transform.LookAt(playerTransform);
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //direction.y = 0;
        //transform.right = direction.normalized;
    }
}
