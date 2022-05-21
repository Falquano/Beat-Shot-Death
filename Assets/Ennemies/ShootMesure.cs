using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
using UnityEditor.VFX;
using UnityEditor.VFX.UI;

public class ShootMesure : Mesure
{
    [Header("Tir")]
    [SerializeField] private int damageEnnemy = 30;
    [SerializeField] private int damagePlayer = 1;
    // j'ai du rajouter �a, c'est la distance max des pistolets
    [SerializeField] private float range = 100f;
    [SerializeField] LayerMask EnnemyMask;
    [SerializeField] public UnityEvent<ShotInfo> onShoot;
    [SerializeField] private float rayOffset = .7f;

    [Header("FX")]
    [SerializeField] private GameObject zapLinePrefab;
    //[SerializeField] private VisualEffect FuturLinePrefab;
    [SerializeField] private Transform barrel;
    [SerializeField] private GameObject impactParticlesPrefab;
    [SerializeField] private float shotImpactSize = .6f;

    [SerializeField] private PlayerHealthSystem scriptPlayerHealth;

    

    private void OnEnable()
    {
        
        tempo.onTimeToShoot.AddListener(Shoot);
        //animator.SetBool("Aiming", true);
    }

    private void OnDisable()
    {
        tempo.onTimeToShoot.RemoveListener(Shoot);
        //animator.SetBool("Aiming", false);
    }

    private void Shoot()
    {
        if (behavior.Player == null || PlayerisDead == true)
            return;

        //Si l'ennemy est trop loin du player il ne vise pas, donc ne fait pas de trait vfx rouge
        if (Vector3.Distance(transform.position, Player.transform.position) > DistanceMaxShoot)
        {
            return;
        }

        //on calcul la direction entre l'ennemi et le player
        Vector3 ShootToPlayer = behavior.Player.transform.position  - transform.position;

        //On tire un raycast de l'ennemi (+ une distance pour pas qu'il se tire dessus) dans le sens de sa direction
        Ray ray = new Ray(transform.position + transform.forward * rayOffset + new Vector3(0, 2,0), transform.forward);

        ShotInfo shotInfo = new ShotInfo()
        {
            Quality = ShotQuality.Good,
            StartPos = barrel.position,
            ShotObject = null,
            EndNormal = Vector3.zero,
            EndPos = barrel.position
        };

        
        
        if (Physics.Raycast(ray, out RaycastHit hitInfo, range, EnnemyMask))
		{
            

            if(hitInfo.collider.tag == "Player")
            {
                PlayerHealthSystem PlayerHealth = hitInfo.collider.GetComponent<PlayerHealthSystem>();
                PlayerHealth.DealDamage(damagePlayer);
            }
            
            else if(hitInfo.collider.tag == "Ennemy")
            {
                HealthSystem targetHealth = hitInfo.collider.GetComponent<HealthSystem>();
                targetHealth.DealDamage(damageEnnemy);
            }
            else if (hitInfo.collider.tag == "Barriere")
            {
                BarriereAnim barriere = hitInfo.transform.GetComponent<BarriereAnim>();
                barriere.ShotCollision(hitInfo.normal);
            }


            shotInfo.EndPos = hitInfo.point; 
            shotInfo.EndNormal = hitInfo.normal;
            shotInfo.ShotObject = hitInfo.collider.gameObject;
		}

        // Lorsque c'est bon, remplace Vector3.zero avec le point de contact du tir

        onShoot.Invoke(shotInfo);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, DistanceMaxShoot);
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
