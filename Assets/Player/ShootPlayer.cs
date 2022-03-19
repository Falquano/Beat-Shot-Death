using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;

public class ShootPlayer : MonoBehaviour
{
    // Ce que ça veut dire c'est que on peut accéder à MousePosition de n'importe où mais on ne peut la modifier que dans cette classe.
    public Vector2 MouseScreenPosition { get; private set; }
    public Vector3 MouseWorldPosition;

    [SerializeField] private GameObject myBulletSpawnPoint;

    [SerializeField] LayerMask TheMask;
    [SerializeField] private LayerMask pointerLayerMask;

    // j'ai du rajouter ça, c'est la distance max des pistolets
    [SerializeField] private float range = 100f;

    // Une liste d'empties depuis lesquels le joueur tire
    [SerializeField] private Transform[] Barrels;
    // Une variable qui va de pistolet en pistolet pour alterner lequel tire
    private int barrelIndex;

    // Un event qui s'active quand on tire et envoie les données du tir. Le fonctionnement des events a été bien expliqué par @Céleste dans le channel de prog je crois
    [SerializeField] public UnityEvent<ShotInfo> OnShotEvent = new UnityEvent<ShotInfo>();
    [SerializeField] public UnityEvent OnDecreasingCombo = new UnityEvent();

    [SerializeField] private int combo = 0;
    [SerializeField] private int maxCombo = 100;
    /// <summary>
    /// Évènement invoqué lorsque la valeur de combo change. Envoie la nouvelle valeur suivie de maxCombo.
    /// </summary>
    [SerializeField] public UnityEvent<int, int> onComboChange = new UnityEvent<int, int>();

    [SerializeField] private TempoManager tempoManager;


    //je ne suis pas sur de cette variable
    [SerializeField] public bool CheckShootisOk = true ;


    //variable de combo que l'on veux retirer
    [SerializeField] private int comboMalusNoShot;
    [SerializeField] private int comboBonusPerfectShot;
    [SerializeField] private int comboBonusOkayShot;
    [SerializeField] private int comboMalusFailedShot;
    [SerializeField] private int comboDecrease;

    // Update is called once per frame
    void Update()
    {
        //calcul à chaque frame de la position de la souris à son dernier déplacement dans le monde.
        Ray pointerRay = Camera.main.ScreenPointToRay(MouseScreenPosition);
        if (Physics.Raycast(pointerRay, out RaycastHit hitInfo, float.MaxValue, pointerLayerMask))
        {
            MouseWorldPosition = hitInfo.point;
            Vector3 direction = MouseWorldPosition - transform.position;
            direction.y = 0;
            transform.forward = direction.normalized;
            Debug.DrawRay(transform.position, transform.right * 4, Color.white);
        } else
		{
            MouseWorldPosition = ExpandToGround(pointerRay.origin, pointerRay.direction, transform.position.y);
            Vector3 direction = MouseWorldPosition - transform.position;
            direction.y = 0;
            transform.forward = direction.normalized;
            Debug.DrawRay(transform.position, transform.right * 4, Color.white);
        }

        tempoManager.Combo = combo;
    }

    public static Vector3 ExpandToGround(Vector3 origin, Vector3 direction, float height)
	{
        float mod = direction.y / direction.magnitude * (height - origin.y);
        return origin + direction * mod;
	}

    public void LookAt(CallbackContext callBack)
    {
        //récupération de la position de la souris par rapport à l'écran
        MouseScreenPosition = callBack.ReadValue<Vector2>();
        MouseScreenPosition = new Vector2(
            Mathf.Clamp(MouseScreenPosition.x, 0, Camera.main.pixelWidth), 
            Mathf.Clamp(MouseScreenPosition.y, 0, Camera.main.pixelHeight));
    }

    public void OnFire(CallbackContext callBack)
    {
        //si lorsque la fonction est appelée, le bouton est appuyé donc Fire = 1
        if (callBack.performed )
        {
            if(CheckShootisOk == true)
            {
                Shoot();
            }
        }
    }

    public void CheckPreviousShoot()
    {
        
        //si checkshootisok est true au début de la mesure c'est qu'on a pas tiré
        if (CheckShootisOk == true)
        {
             //La surchauffe descend de 10 si le joueur n'a pas tirer le tempo précédent
             combo = Mathf.Clamp(combo + comboMalusNoShot, 0, maxCombo);
             onComboChange.Invoke(combo, maxCombo);   
        }

        //On passe la var de check de tir à true pour que le joueur puisse tirer dans cette nouvelle mesure
        CheckShootisOk = true;
    }


    public void DecreaseCombo()
    {
        //Le combo descend si on appuye sur R
        combo = Mathf.Clamp(combo + comboDecrease, 0, maxCombo);
        onComboChange.Invoke(combo, maxCombo);
        OnDecreasingCombo.Invoke();

    }

   

    public void Shoot()
    {
        //on calcul la direction entre le player et la souris 
        //Vector2 DirectionShoot = Camera.main.ScreenToWorldPoint(MouseScreenPosition) - transform.position;
        Vector3 DirectionShoot = transform.forward;

        Ray ray = new Ray(transform.position, DirectionShoot.normalized);
        //on créer un raycast du player dans la direction de la souris de distance max sur un mask sans le player lui-même
        if (Physics.Raycast(ray, out RaycastHit RayShoot , range, TheMask))
		{
            //Debug
            Debug.DrawLine(transform.position, RayShoot.point, Color.red, 0.2f);

            //On vérif si le tir est dans le cadran du tir ok
        
            switch (tempoManager.ShotQualityNow())
            {
                case ShotQuality.Failed:
                    FailedShot();
                    break;
                case ShotQuality.Okay:
                    OkayShot(RayShoot, DirectionShoot.normalized);
                    break;
                case ShotQuality.Perfect:
                    PerfectShot(RayShoot, DirectionShoot.normalized);
                    break;
            }
		}


        barrelIndex = (barrelIndex + 1) % Barrels.Length;
    }

    private void PerfectShot(RaycastHit RayShoot, Vector2 direction)
    {
        //On vérifie si il collide avec un élément et si cet élément possède le tag ennemy
        if (RayShoot.collider != null && RayShoot.transform.tag == "Ennemy")
        {
            //On récupère le script behavior de l'ennemy touché
            HealthSystem targetHealth = RayShoot.transform.GetComponent<HealthSystem>();

            //alors on prend le script de l'ennemy touché et on lui retire 30 pts
            targetHealth.DealDamage(10 + combo / 5);
        }

        //La surchauffe augmente de 10
        combo = Mathf.Clamp(combo + comboBonusPerfectShot, 0, maxCombo);
        onComboChange.Invoke(combo, maxCombo);

        ShotInfo info = new ShotInfo()
        {
            StartPos = Barrels[barrelIndex].position,
            EndPos = RaycastHitPoint(RayShoot, direction),
            Quality = ShotQuality.Perfect,
            ShotObject = RayShoot.transform == null ? null : RayShoot.transform.gameObject,
            EndNormal = RayShoot.normal
        };
        OnShotEvent.Invoke(info);
        CheckShootisOk = false;
        print("check = " + CheckShootisOk);
    }

    private void OkayShot(RaycastHit RayShoot, Vector2 direction)
    {
        //On vérifie si il collide avec un élément et si cet élément possède le tag ennemy
        if (RayShoot.collider != null && RayShoot.transform.tag == "Ennemy")
        {
            //On récupère le script behavior de l'ennemy touché
            HealthSystem targetHealth = RayShoot.transform.GetComponent<HealthSystem>();

            //Si il n'est pas dans tir parfait mais juste dans le tir ok, on prend le script de l'ennemy et on lui retire 10 pts
            targetHealth.DealDamage(10);
        }

        //La surchauffe descend de 10
        combo = Mathf.Clamp(combo + comboBonusOkayShot, 0, maxCombo);
        onComboChange.Invoke(combo, maxCombo);

        ShotInfo info = new ShotInfo()
        {
            StartPos = Barrels[barrelIndex].position,
            EndPos = RaycastHitPoint(RayShoot, direction),
            Quality = ShotQuality.Okay,
            // test ? valeur si vrai : valeur si faux
            // c'est pas giga élégant mais parfois c'est juste pratique
            ShotObject = RayShoot.transform == null ? null : RayShoot.transform.gameObject,
            EndNormal = RayShoot.normal
        };
        OnShotEvent.Invoke(info);
        CheckShootisOk = false;
    }

    private void FailedShot()
    {
        //La surchauffe descend de 30 car le tir est raté.
        combo = Mathf.Clamp(combo + comboMalusFailedShot, 0, maxCombo);
        onComboChange.Invoke(combo, maxCombo);

        ShotInfo info = new ShotInfo()
        {
            StartPos = Barrels[barrelIndex].position,
            EndPos = Barrels[barrelIndex].position,
            Quality = ShotQuality.Failed,
            ShotObject = null,
            EndNormal = Vector2.zero
        };
        OnShotEvent.Invoke(info);
        CheckShootisOk = false;
    }

    public Vector3 RaycastHitPoint(RaycastHit hit, Vector3 direction)
    {
        if (hit.collider != null)
            return hit.point;

        return transform.position + direction * range;
    }

    public void Overheated(CallbackContext callBack)
    {
        //lorsque l'on clic sur R la surchauffe descend de 10 (celle-ci est clamper de 0 à 100)
        if (callBack.performed)
        {
            combo -= 10;
            combo = Mathf.Clamp(combo, 0, maxCombo);
            onComboChange.Invoke(combo, maxCombo);
            
        }
    }
}

// Un type qui contient toutes les infos sur un tir. Comme ça on peut l'envoyer aux systèmes de particules et tout ça
public struct ShotInfo
{
    public Vector3 StartPos { get; set; }
    public Vector3 EndPos { get; set; }
    public ShotQuality Quality { get; set; }
    public GameObject ShotObject { get; set; }
    public Vector3 EndNormal { get; set; }
}

// Une liste de qualités de tirs pour facilement avoir l'info
public enum ShotQuality
{
    Failed,
    Okay,
    Perfect
}