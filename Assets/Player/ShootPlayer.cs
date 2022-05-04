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
    [SerializeField] public UnityEvent<bool, int, int> OnShotEvent = new UnityEvent<bool , int, int>();


    [SerializeField] public int combo = 0;
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
    [SerializeField] private int comboGoodShotMod = 0;
    [SerializeField] private int comboDecrease;

    [SerializeField] private int perfectShotDamage = 20;
    [SerializeField] private int goodShotDamage = 10;
   

    public bool inTempo;
    private int damage;
    //check de si le joueur tir quand la croix est rouge


    [SerializeField] private int MesureBeforeComboDecreasing;
    private int numberOfNonShoot = 0;

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

    public static Vector3 ExpandToGround(Vector3 origin, Vector3 direction, float height) //A quoi sa sert ?
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
                OnShoot();
            }
        }
    }

    public void CheckPreviousShoot()
    {
        //Cette fonction est appel� tout les 2 beats


        //si checkshootisok est true au d�but de la mesure c'est qu'on a pas tir�
        if (CheckShootisOk == true)
        {
            //Cette variable compte le nombre de mesure o� le joueur n'a pas tir�
            numberOfNonShoot += 1;
      
        }
        

        if ( numberOfNonShoot >= MesureBeforeComboDecreasing)
        {
            //La surchauffe descend de 10 si le joueur n'a pas tirer X fois d'affil� 
            combo = Mathf.Clamp(combo + comboNoShotMod, 0, maxCombo);
            onComboChange.Invoke(combo, maxCombo);
            

            
        }

        //On passe la var de check de tir � true pour que le joueur puisse tirer dans cette nouvelle mesure
        CheckShootisOk = true;
    }


    public void OnComboIncrease() 
    {
        //Je ne sais pas pk mais �a augmente de plein 
        combo += 15;
        print(combo);
        //On annonce au monde que le combo a chang�
        onComboChange.Invoke(combo, maxCombo);
    }
   


    public Vector3 RaycastHitPoint(RaycastHit hit, Vector3 direction)
    {
        if (hit.collider != null)
            return hit.point;

        return transform.position + direction * range;
    }

    

    private int ComboDamageBonus(int baseDamage)
    {
        if (combo >= 0 && combo <= 10)
        {
            return baseDamage;
        }
        else if (combo > 10 && combo <= 30)
        {
            return (int)((float)baseDamage * 1.5f);
        }
        else if (combo > 30 && combo <= 60)
        {
            return baseDamage * 2;
        }
        else if (combo > 60 && combo <= 75)
        {
            return baseDamage * 3;
        }
        else
        {
            return baseDamage * 4;
        }
    }


    

    public void OnShoot()
    {
        
        //Comme le player regarde d�j� dans la direction de la souris, son forward est dans le sens de celle-ci
        Vector3 DirectionShoot = transform.forward;

        Ray ray = new Ray(transform.position, DirectionShoot.normalized);
        //on cr�er un raycast du player dans le sens de sa direction
        if (Physics.Raycast(ray, out RaycastHit RayShoot, range, TheMask))
        {
            //Debug
            Debug.DrawLine(transform.position, RayShoot.point, Color.red, 0.2f);

            

            //On v�rifie si il collide avec un �l�ment et si cet �l�ment poss�de le tag ennemy
            if (RayShoot.collider != null && RayShoot.transform.tag == "Ennemy")
            {
                //On tir sur un ennemi (peut importe les d�g�ts), alors on ne descendra pas en combo)
                numberOfNonShoot = 0;

                //On r�cup�re le script behavior de l'ennemy touch�
                HealthSystem targetHealth = RayShoot.transform.GetComponent<HealthSystem>();

                if(inTempo == true)
                {
                    //Si le player tir quand la croix est rouge alors:

                    //Il augmente en combo
                    combo = Mathf.Clamp(combo + comboPerfectShotMod, 0, maxCombo);
                    //Il fait des d�g�ts parfait (20) plus le combo actuel
                    damage = ComboDamageBonus(perfectShotDamage);
                    targetHealth.DealDamage(damage);
                }
                else if(inTempo == false)
                {
                    //Si le player tir quand la croix n'est pas rouge:

                    //Il fait des d�g�ts good (10) plus le combo actuel
                    damage = ComboDamageBonus(goodShotDamage);
                    targetHealth.DealDamage(damage);
                }


            }

            //On v�rifie si il collide avec un �l�ment et si cet �l�ment poss�de le tag Button
            if (RayShoot.collider != null && RayShoot.transform.tag == "Button")
            {
                //On tir sur un ennemi (peut importe les d�g�ts), alors on ne descendra pas en combo)
                numberOfNonShoot = 0;

                //On r�cup�re l'animator du button
                Animator AnimButton = RayShoot.transform.GetComponent<Animator>();
                ButtonJustShoot ButtonScript = RayShoot.transform.GetComponent<ButtonJustShoot>();

                if (inTempo == true)
                {
                    //Si le player tir quand la croix est rouge alors:

                    //Il augmente en combo
                    combo = Mathf.Clamp(combo + comboPerfectShotMod, 0, maxCombo);

                    AnimButton.SetInteger("QualityShoot", 2);

                }
                else if (inTempo == false)
                {
                    //Si le player tir quand la croix n'est pas rouge:

                    combo = Mathf.Clamp(combo + comboGoodShotMod, 0, maxCombo);

                    //Changement de l'anim du boutton
                    AnimButton.SetInteger("QualityShoot", 1);
                }

                
                


            }

            if (RayShoot.collider != null && RayShoot.transform.tag != "Button" & RayShoot.transform.tag != "Ennemy")
            {
                //Si le joueur tir sur aucun de ces deux �l�ments, alors son tir est comptabilis� comme nul est compte comme un non tir, le combo descendra
                numberOfNonShoot += 1;
            }

            
            // On annonce au monde qu'un tir a �t� effectu� avec les infos pr�c�dentes
            OnShotEvent.Invoke(inTempo, combo, damage);
            // On d�sactive le tir pour cette mesure
            CheckShootisOk = false;
        }

        //On annonce au monde que le combo a chang�
        onComboChange.Invoke(combo, maxCombo);

        // On change de pistolet
        barrelIndex = (barrelIndex + 1) % Barrels.Length;
       
    }

    public void InTempo(bool tempoEventAnim)
    {
        //Valide quand la croix devient rouge et quand elle se r�initialise
        inTempo = tempoEventAnim;
    }

    


}


