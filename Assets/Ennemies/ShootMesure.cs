using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootMesure : Mesure
{
    [Header("Tir")]
    [SerializeField] private int damage = 30;
    // j'ai du rajouter ça, c'est la distance max des pistolets
    [SerializeField] private float range = 100f;
    [SerializeField] LayerMask EnnemyMask;
    [SerializeField] public UnityEvent<ShotInfo> onShoot;
    [SerializeField] private float rayOffset = .7f;

    [Header("FX")]
    [SerializeField] private GameObject zapLinePrefab;
    [SerializeField] private Transform barrel;
    [SerializeField] private GameObject impactParticlesPrefab;
    [SerializeField] private float shotImpactSize = .6f;

    private void OnEnable()
    {
        //Debug.Log("Ennemy is now shooting");
        tempo.onTimeToShoot.AddListener(Shoot);
        animator.SetBool("Aiming", true);
    }

    private void OnDisable()
    {
        tempo.onTimeToShoot.RemoveListener(Shoot);
        animator.SetBool("Aiming", false);
    }

    private void Shoot()
    {
        if (behavior.Player == null)
            return;

        // Ici il faut ajouter le tir ennemi
        // Fait le au raycast, pas au projectile
        //on calcul la direction entre l'ennemi et le player
        Vector3 ShootToPlayer = behavior.Player.transform.position  - transform.position;

        Ray ray = new Ray(transform.position + transform.right * rayOffset, transform.right);

        ShotInfo shotInfo = new ShotInfo()
        {
            Quality = ShotQuality.Okay,
            StartPos = barrel.position,
            ShotObject = null,
            EndNormal = Vector3.zero,
            EndPos = barrel.position
        };

        //on créer un raycast du player dans la direction de la souris de distance max sur un mask sans le player lui-même
        //RaycastHit RayShootEnnemy = Physics.Raycast(transform.position + transform.right * 1.2f, transform.right, range, EnnemyMask);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, range, EnnemyMask))
		{
            HealthSystem targetHealth = hitInfo.collider.GetComponent<HealthSystem>();
            if (targetHealth != null)
            {
                targetHealth.DealDamage(damage);
            }

            shotInfo.EndPos = hitInfo.point;
            shotInfo.EndNormal = hitInfo.normal;
            shotInfo.ShotObject = hitInfo.collider.gameObject;
		}

        // Lorsque c'est bon, remplace Vector3.zero avec le point de contact du tir

        onShoot.Invoke(shotInfo);
    }

    public void ShotFX(ShotInfo info)
	{
        ZapLine(info.StartPos, info.EndPos);
        Impact(info.EndPos, info.EndNormal, info.Quality);
	}

    private void ZapLine(Vector3 start, Vector3 end)
    {
        LineRenderer line = Instantiate(zapLinePrefab, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
        line.SetPosition(0, start);
        line.SetPosition(1, end);
    }


    private void Impact(Vector3 position, Vector3 normal, ShotQuality quality)
    {
        Transform particle = Instantiate(impactParticlesPrefab, position, Quaternion.identity).transform;
        Vector3 size = Vector3.one * shotImpactSize;
        particle.localScale = size;
        particle.up = normal;
    }
}
