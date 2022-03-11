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

        //R�cup�ration du transform du player
        Transform playerTransform = behavior.Player.GetComponent<Transform>();

        //Calcul du vector entre l'ennemi et le player
        Vector3 direction = playerTransform.position - transform.position;

        //L'ennemi regarde le player
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        
    }
}
