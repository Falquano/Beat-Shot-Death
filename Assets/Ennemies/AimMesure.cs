using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
using UnityEditor.VFX;
using UnityEditor.VFX.UI;
using System.Collections;

public class AimMesure : Mesure
{
    public float rotationSpeed;
    [SerializeField][Range(0f, 1f)] private float aimingTime = .5f;

    [SerializeField] public UnityEvent onAim;
    [SerializeField] public float distanceMaxWithPlayer;
    public VisualEffect FuturLine; 



    private void OnEnable()
    {

        onAim.Invoke();

    }

    private void OnDisable()
    {
        
    }

    private void Update()
    {
        if (behavior.Player == null || PlayerisDead == true)
            return;

        if (tempo.Tempo > aimingTime)
            return;

        //R�cup�ration du transform du player
        Transform playerTransform = behavior.Player.GetComponent<Transform>();
        

        //Calcul du vector entre l'ennemi et le player
        Vector3 direction = playerTransform.position - transform.position;

        //L'ennemi regarde le player
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        //Check si la distance avec le player n'est pas trop grande
        float distancePlayerEnnemy = Vector3.Distance(transform.position, playerTransform.position) ;
        if(distancePlayerEnnemy> distanceMaxWithPlayer)
        {
            return;
        }
        //VFX LineShoot nécessaire pour les tourelles
        FuturLine.Play();
    }


    
}
