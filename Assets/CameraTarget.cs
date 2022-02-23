using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private ShootPlayer player;
    // Avec Range je v�rifie que dans l'�diteur on ne puisse pas mettre moins de 0 ou plus de 1
    [SerializeField] [Range(0f, 1f)] private float bias = .5f;

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
        // Je place la cible entre le joueur et le curseur, avec un biais r�glable
        transform.position = ((Vector2)player.transform.position * (1f - bias) + player.MouseWorldPosition * bias);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, .2f);
    }
}
