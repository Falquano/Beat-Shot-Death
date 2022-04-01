using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimMesure : Mesure
{
    public float rotationSpeed;
    [SerializeField][Range(0f, 1f)] private float aimingTime = .5f;

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

        if (tempo.Tempo > aimingTime)
            return;

        //Récupération du transform du player
        Transform playerTransform = behavior.Player.GetComponent<Transform>();

        //Calcul du vector entre l'ennemi et le player
        Vector3 direction = playerTransform.position - transform.position;

        //L'ennemi regarde le player
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
