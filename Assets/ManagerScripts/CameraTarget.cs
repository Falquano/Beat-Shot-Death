using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private ShootPlayer player;
    // Avec Range je vérifie que dans l'éditeur on ne puisse pas mettre moins de 0 ou plus de 1
    [SerializeField] [Range(0f, 1f)] private float bias = .5f;

    private Vector3 currentVelocity;
    [SerializeField] private float smoothTime = .2f;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogWarning("Camera target requiert un joueur !");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Je place la cible entre le joueur et le curseur, avec un biais réglable
        Vector3 newPos = (player.transform.position * (1f - bias) + player.MouseWorldPosition * bias);
        newPos.y = player.transform.position.y;
        transform.position = newPos;
        //transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref currentVelocity, smoothTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, .2f);
    }
}
