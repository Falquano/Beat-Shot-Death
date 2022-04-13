using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;

public class ShootPlayer : MonoBehaviour
{
    // Ce que �a veut dire c'est que on peut acc�der � MousePosition de n'importe o� mais on ne peut la modifier que dans cette classe.
    public Vector2 MouseScreenPosition { get; private set; }
    public Vector3 MouseWorldPosition;

    [SerializeField] private GameObject myBulletSpawnPoint;

    [SerializeField] LayerMask TheMask;
    [SerializeField] private LayerMask pointerLayerMask;

    // j'ai du rajouter �a, c'est la distance max des pistolets
    [SerializeField] private float range = 100f;

    // Une liste d'empties depuis lesquels le joueur tire
    [SerializeField] private Transform[] Barrels;
    // Une variable qui va de pistolet en pistolet pour alterner lequel tire
    private int barrelIndex;

    // Un event qui s'active quand on tire et envoie les donn�es du tir. Le fonctionnement des events a �t� bien expliqu� par @C�leste dans le channel de prog je crois
    [SerializeField] public UnityEvent<ShotInfo> OnShotEvent = new UnityEvent<ShotInfo>();
    [SerializeField] public UnityEvent OnDecreasingCombo = new UnityEvent();

    [SerializeField] private int combo = 0;
    [SerializeField] private int maxCombo = 100;
    /// <summary>
    /// �v�nement invoqu� lorsque la valeur de combo change. Envoie la nouvelle valeur suivie de maxCombo.
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
        //calcul � chaque frame de la position de la souris � son dernier d�placement dans le monde.
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
        //r�cup�ration de la position de la souris par rapport � l'�cran
        MouseScreenPosition = callBack.ReadValue<Vector2>();
        MouseScreenPosition = new Vector2(
            Mathf.Clamp(MouseScreenPosition.x, 0, Camera.main.pixelWidth), 
            Mathf.Clamp(MouseScreenPosition.y, 0, Camera.main.pixelHeight));
    }

    public void OnFire(CallbackContext callBack)
    {
        //si lorsque la fonction est appel�e, le bouton est appuy� donc Fire = 1
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
        //Cette fonction est appel� � chaque d�but de temps

       


        //si checkshootisok est true au d�but de la mesure c'est qu'on a pas tir�
        if (CheckShootisOk == true)
        {
             //La surchauffe descend de 10 si le joueur n'a pas tirer le tempo pr�c�dent
             combo = Mathf.Clamp(combo + comboNoShotMod, 0, maxCombo);
             onComboChange.Invoke(combo, maxCombo);   
        }

        //On passe la var de check de tir � true pour que le joueur puisse tirer dans cette nouvelle mesure
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
        //on cr�er un raycast du player dans la direction de la souris de distance max sur un mask sans le player lui-m�me
        if (Physics.Raycast(ray, out RaycastHit RayShoot , range, TheMask))
		{
            //Debug
            Debug.DrawLine(transform.position, RayShoot.point, Color.red, 0.2f);

            //On v�rif si le tir est dans le cadran du tir ok

            //On v�rifie si il collide avec un �l�ment et si cet �l�ment poss�de le tag ennemy
            if (RayShoot.collider != null && RayShoot.transform.tag == "Ennemy")
            {
                //On r�cup�re le script behavior de l'ennemy touch�
                HealthSystem targetHealth = RayShoot.transform.GetComponent<HealthSystem>();

                // Selon la qualit� on envoie les d�gats appropri�s et on augmente ou diminue le combo
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

            //On v�rifie si il collide avec un �l�ment et si cet �l�ment poss�de le tag Button
            if (RayShoot.collider != null && RayShoot.transform.tag == "Button")
            {
                

                //On r�cup�re l'animator du button
                Animator AnimButton = RayShoot.transform.GetComponent<Animator>();
                ButtonJustShoot ButtonScript = RayShoot.transform.GetComponent<ButtonJustShoot>();

                

                // Selon la qualit� on change la couleur du bouton et on augmente ou diminue le combo
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

            // On cr�e un "rapport de tir" qui contient toutes les infos n�cessaires au lancement d'FX, sons et tout �a
            ShotInfo info = new ShotInfo()
            {
                StartPos = Barrels[barrelIndex].position,
                EndPos = RaycastHitPoint(RayShoot, DirectionShoot.normalized),
                Quality = quality,
                ShotObject = RayShoot.transform == null ? null : RayShoot.transform.gameObject,
                EndNormal = RayShoot.normal
            };
            // On annonce au monde qu'un tir a �t� effectu� avec les infos pr�c�dentes
            OnShotEvent.Invoke(info);
            // On d�sactive le tir pour cette mesure
            CheckShootisOk = false;
        }

        //On annonce au monde que le combo a chang�
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
        //lorsque l'on clic sur R la surchauffe descend de 10 (celle-ci est clamper de 0 � 100)
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

// Un type qui contient toutes les infos sur un tir. Comme �a on peut l'envoyer aux syst�mes de particules et tout �a
public struct ShotInfo
{
    public Vector3 StartPos { get; set; }
    public Vector3 EndPos { get; set; }
    public ShotQuality Quality { get; set; }
    public GameObject ShotObject { get; set; }
    public Vector3 EndNormal { get; set; }
}

// Une liste de qualit�s de tirs pour facilement avoir l'info
public enum ShotQuality
{
    Bad,
    Good,
    Perfect
}