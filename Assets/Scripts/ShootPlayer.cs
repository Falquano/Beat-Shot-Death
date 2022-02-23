using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;

public class ShootPlayer : MonoBehaviour
{
    // Ce que �a veut dire c'est que on peut acc�der � MousePosition de n'importe o� mais on ne peut la modifier que dans cette classe.
    public Vector2 MousePosition { get; private set; }
    [SerializeField] Camera cam;

    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject myBulletSpawnPoint;

    [SerializeField] private float SpeedBullet;

    [SerializeField] LayerMask TheMask;

    //variable pour le tir en tempo
    [SerializeField] private float ObjectiveShoot = 0.5f;
    [SerializeField] private float MarginPerfect = 0.1f;
    [SerializeField] private float MarginOk = 0.3f;
    private float TimerTempo;
    [SerializeField] private float TempoDuration;
    public float Tempo => TimerTempo / TempoDuration;

    // Une liste d'empties depuis lesquels le joueur tire
    [SerializeField] private Transform[] Barrels;
    // Une variable qui va de pistolet en pistolet pour alterner lequel tire
    private int barrelIndex;

    // Un event qui s'active quand on tire et envoie les donn�es du tir. Le fonctionnement des events a �t� bien expliqu� par @C�leste dans le channel de prog je crois
    [SerializeField] public UnityEvent<ShotInfo> OnShotEvent;

    private void Start()
    {
        OnShotEvent = new UnityEvent<ShotInfo>();
    }

    public void LookAt(CallbackContext callBack)
    {
        //r�cup�ration de la position de la souris par rapport � l'�cran
        MousePosition = callBack.ReadValue<Vector2>();
        

    }

    public void OnFire(CallbackContext callBack)
    {
        //si lorsque la fonction est appel�e, le bouton est appuy� donc Fire = 1
        if (callBack.performed)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        //on calcul la direction entre le player et la souris 
        Vector2 DirectionShoot = Camera.main.ScreenToWorldPoint(MousePosition) - transform.position;

        //on cr�er un raycast du player dans la direction de la souris de distance max sur un mask sans le player lui-m�me
        RaycastHit2D RayShoot = Physics2D.Raycast(transform.position, DirectionShoot.normalized, float.MaxValue, TheMask);

        //Debug
        Debug.DrawLine(transform.position, RayShoot.point, Color.red, 0.2f);

        //On v�rif si le tir est dans le cadran du tir ok
        if (Tempo >= ObjectiveShoot - MarginOk && Tempo <= ObjectiveShoot + MarginOk)
        {
            //si le tir est dans le cadran tir parfait
            if (Tempo >= ObjectiveShoot - MarginPerfect && Tempo <= ObjectiveShoot + MarginPerfect)
            {
                PerfectShot(RayShoot);
            }
            else
            {
                OkayShot(RayShoot);
            }
        }

        barrelIndex = (barrelIndex + 1) % Barrels.Length;
    }

    private void PerfectShot(RaycastHit2D RayShoot)
    {
        //On v�rifie si il collide avec un �l�ment et si cet �l�ment poss�de le tag ennemy
        if (RayShoot.collider != null && RayShoot.transform.tag == "Ennemy")
        {
            //On r�cup�re le script behavior de l'ennemy touch�
            EnnemyBehavior myEnnemyScript = RayShoot.transform.GetComponent<EnnemyBehavior>();

            //alors on prend le script de l'ennemy touch� et on lui retire 30 pts
            myEnnemyScript.DamageEnnemy(30);
        }

        ShotInfo info = new ShotInfo()
        {
            StartPos = Barrels[barrelIndex].position,
            EndPos = RayShoot.point,
            Quality = ShotQuality.Perfect,
            ShotObject = RayShoot.transform.gameObject
        };
        OnShotEvent.Invoke(info);
    }

    private void OkayShot(RaycastHit2D RayShoot)
    {
        //On v�rifie si il collide avec un �l�ment et si cet �l�ment poss�de le tag ennemy
        if (RayShoot.collider != null && RayShoot.transform.tag == "Ennemy")
        {
            //On r�cup�re le script behavior de l'ennemy touch�
            EnnemyBehavior myEnnemyScript = RayShoot.transform.GetComponent<EnnemyBehavior>();

            //Si il n'est pas dans tir parfait mais juste dans le tir ok, on prend le script de l'ennemy et on lui retire 10 pts
            myEnnemyScript.DamageEnnemy(10);
        }
        
        ShotInfo info = new ShotInfo()
        {
            StartPos = Barrels[barrelIndex].position,
            EndPos = RayShoot.point,
            Quality = ShotQuality.Okay,
            ShotObject = RayShoot.transform.gameObject
        };
        OnShotEvent.Invoke(info);
    }

    private void FailedShot()
    {
        ShotInfo info = new ShotInfo()
        {
            StartPos = Barrels[barrelIndex].position,
            EndPos = Vector2.zero,
            Quality = ShotQuality.Okay,
            ShotObject = null
        };
        OnShotEvent.Invoke(info);
    }

    // Update is called once per frame
    void Update()
    {
        //calcul � chaque frame de la position de la souris � son dernier d�placement dans le monde.
        Vector3 screenToWorldPosition = Camera.main.ScreenToWorldPoint(MousePosition);

        float AngleRad = Mathf.Atan2(screenToWorldPosition.y - transform.position.y, screenToWorldPosition.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);


        TimerTempo = (TimerTempo + Time.deltaTime) % TempoDuration;
       


    }
}

// Un type qui contient toutes les infos sur un tir. Comme �a on peut l'envoyer aux syst�mes de particules et tout �a
public struct ShotInfo
{
    public Vector2 StartPos { get; set; }
    public Vector2 EndPos { get; set; }
    public ShotQuality Quality { get; set; }
    public GameObject ShotObject { get; set; }
}

// Une liste de qualit�s de tirs pour facilement avoir l'info
public enum ShotQuality
{
    Failed,
    Okay,
    Perfect
}