using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;

public class ShootPlayer : MonoBehaviour
{
    // Ce que ça veut dire c'est que on peut accéder à MousePosition de n'importe où mais on ne peut la modifier que dans cette classe.
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

    // Un event qui s'active quand on tire et envoie les données du tir. Le fonctionnement des events a été bien expliqué par @Céleste dans le channel de prog je crois
    [SerializeField] public UnityEvent<ShotInfo> OnShotEvent;

    private void Start()
    {
        OnShotEvent = new UnityEvent<ShotInfo>();
    }

    public void LookAt(CallbackContext callBack)
    {
        //récupération de la position de la souris par rapport à l'écran
        MousePosition = callBack.ReadValue<Vector2>();
        

    }

    public void OnFire(CallbackContext callBack)
    {
        //si lorsque la fonction est appelée, le bouton est appuyé donc Fire = 1
        if (callBack.performed)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        //on calcul la direction entre le player et la souris 
        Vector2 DirectionShoot = Camera.main.ScreenToWorldPoint(MousePosition) - transform.position;

        //on créer un raycast du player dans la direction de la souris de distance max sur un mask sans le player lui-même
        RaycastHit2D RayShoot = Physics2D.Raycast(transform.position, DirectionShoot.normalized, float.MaxValue, TheMask);

        //Debug
        Debug.DrawLine(transform.position, RayShoot.point, Color.red, 0.2f);

        //On vérif si le tir est dans le cadran du tir ok
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
        //On vérifie si il collide avec un élément et si cet élément possède le tag ennemy
        if (RayShoot.collider != null && RayShoot.transform.tag == "Ennemy")
        {
            //On récupère le script behavior de l'ennemy touché
            EnnemyBehavior myEnnemyScript = RayShoot.transform.GetComponent<EnnemyBehavior>();

            //alors on prend le script de l'ennemy touché et on lui retire 30 pts
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
        //On vérifie si il collide avec un élément et si cet élément possède le tag ennemy
        if (RayShoot.collider != null && RayShoot.transform.tag == "Ennemy")
        {
            //On récupère le script behavior de l'ennemy touché
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
        //calcul à chaque frame de la position de la souris à son dernier déplacement dans le monde.
        Vector3 screenToWorldPosition = Camera.main.ScreenToWorldPoint(MousePosition);

        float AngleRad = Mathf.Atan2(screenToWorldPosition.y - transform.position.y, screenToWorldPosition.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);


        TimerTempo = (TimerTempo + Time.deltaTime) % TempoDuration;
       


    }
}

// Un type qui contient toutes les infos sur un tir. Comme ça on peut l'envoyer aux systèmes de particules et tout ça
public struct ShotInfo
{
    public Vector2 StartPos { get; set; }
    public Vector2 EndPos { get; set; }
    public ShotQuality Quality { get; set; }
    public GameObject ShotObject { get; set; }
}

// Une liste de qualités de tirs pour facilement avoir l'info
public enum ShotQuality
{
    Failed,
    Okay,
    Perfect
}