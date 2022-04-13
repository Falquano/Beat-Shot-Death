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
    [SerializeField] private int comboNoShotMod = -16;
    [SerializeField] private int comboPerfectShotMod = 10;
    [SerializeField] private int comboGoodShotMod = 2;
    [SerializeField] private int comboBadShotMod = -8;
    [SerializeField] private int comboDecrease;

    [SerializeField] private int perfectShotDamage = 3;
    [SerializeField] private int goodShotDamage = 2;
    [SerializeField] private int badShotDamage = 1;

    //Appel du script d'animation du player
    [SerializeField] private AnimationInvoker ScriptAnimation;

    [Header("Debug")]
    [SerializeField] private bool logShots = false;


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
        //Cette fonction est appelé à chaque début de temps

       


        //si checkshootisok est true au début de la mesure c'est qu'on a pas tiré
        if (CheckShootisOk == true)
        {
             //La surchauffe descend de 10 si le joueur n'a pas tirer le tempo précédent
             combo = Mathf.Clamp(combo + comboNoShotMod, 0, maxCombo);
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
        ShotQuality quality = tempoManager.ShotQualityNow();
        if (logShots)
        {
            Debug.Log($"Shot triggered at {tempoManager.Tempo.ToString("F3")} => {quality}");
        }


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

            //On vérifie si il collide avec un élément et si cet élément possède le tag ennemy
            if (RayShoot.collider != null && RayShoot.transform.tag == "Ennemy")
            {
                //On récupère le script behavior de l'ennemy touché
                HealthSystem targetHealth = RayShoot.transform.GetComponent<HealthSystem>();

                // Selon la qualité on envoie les dégats appropriés et on augmente ou diminue le combo
                switch (quality)
                {
                    case ShotQuality.Bad:
                        targetHealth.DealDamage(ComboDamageBonus(badShotDamage));
                        combo = Mathf.Clamp(combo + comboBadShotMod, 0, maxCombo);
                        break;
                    case ShotQuality.Good:
                        targetHealth.DealDamage(ComboDamageBonus(goodShotDamage));
                        combo = Mathf.Clamp(combo + comboGoodShotMod, 0, maxCombo);
                        break;
                    case ShotQuality.Perfect:
                        targetHealth.DealDamage(ComboDamageBonus(perfectShotDamage));
                        combo = Mathf.Clamp(combo + comboPerfectShotMod, 0, maxCombo);
                        break;
                }
            }

            //On vérifie si il collide avec un élément et si cet élément possède le tag Button
            if (RayShoot.collider != null && RayShoot.transform.tag == "Button")
            {
                

                //On récupère l'animator du button
                Animator AnimButton = RayShoot.transform.GetComponent<Animator>();
                ButtonJustShoot ButtonScript = RayShoot.transform.GetComponent<ButtonJustShoot>();

                

                // Selon la qualité on change la couleur du bouton et on augmente ou diminue le combo
                switch (quality)
                {
                    case ShotQuality.Bad:
                        AnimButton.SetInteger("QualityShoot", 1); 
                        combo = Mathf.Clamp(combo + comboBadShotMod, 0, maxCombo);
                        print("Orange");
                        break;
                    case ShotQuality.Good:
                        AnimButton.SetInteger("QualityShoot", 2);
                        combo = Mathf.Clamp(combo + comboGoodShotMod, 0, maxCombo);
                        print("Yellow");
                        break;
                    case ShotQuality.Perfect:
                        AnimButton.SetInteger("QualityShoot", 3);
                        combo = Mathf.Clamp(combo + comboPerfectShotMod, 0, maxCombo);
                        ButtonScript.DoorOpening();
                        print("Green");
                        break;
                }
            }

            // On crée un "rapport de tir" qui contient toutes les infos nécessaires au lancement d'FX, sons et tout ça
            ShotInfo info = new ShotInfo()
            {
                StartPos = Barrels[barrelIndex].position,
                EndPos = RaycastHitPoint(RayShoot, DirectionShoot.normalized),
                Quality = quality,
                ShotObject = RayShoot.transform == null ? null : RayShoot.transform.gameObject,
                EndNormal = RayShoot.normal
            };
            // On annonce au monde qu'un tir a été effectué avec les infos précédentes
            OnShotEvent.Invoke(info);
            // On désactive le tir pour cette mesure
            CheckShootisOk = false;
        }

        //On annonce au monde que le combo a changé
        onComboChange.Invoke(combo, maxCombo);

        // On change de pistolet
        barrelIndex = (barrelIndex + 1) % Barrels.Length;
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

    private int ComboDamageBonus(int baseDamage)
    {
        if (combo >= 0 && combo <= 20)
        {
            return baseDamage;
        }
        else if (combo > 20 && combo <= 40)
        {
            return (int)((float)baseDamage * 1.5f);
        }
        else if (combo > 40 && combo <= 70)
        {
            return baseDamage * 2;
        }
        else if (combo > 70 && combo <= 90)
        {
            return baseDamage * 3;
        }
        else
        {
            return baseDamage * 5;
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
    Bad,
    Good,
    Perfect
}