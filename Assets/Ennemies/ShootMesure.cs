using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMesure : Mesure
{
    [SerializeField] private GameObject zapLinePrefab;
    [SerializeField] private Transform barrel;

    private void OnEnable()
    {
        //Debug.Log("Ennemy is now shooting");
        tempo.onTimeToShoot.AddListener(Shoot);
    }

    private void OnDisable()
    {
        tempo.onTimeToShoot.RemoveListener(Shoot);
    }

    private void Shoot()
    {
        // Ici il faut ajouter le tir ennemi
        // Fait le au raycast, pas au projectile



        // Lorsque c'est bon, remplace Vector3.zero avec le point de contact du tir
        ZapLine(barrel.position, Vector3.zero);
    }

    private void ZapLine(Vector3 start, Vector3 end)
    {
        LineRenderer line = Instantiate(zapLinePrefab, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
        line.SetPosition(0, start);
        line.SetPosition(1, end);
    }
}
