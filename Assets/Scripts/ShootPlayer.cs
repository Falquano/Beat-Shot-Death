using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [SerializeField] private int OverHeated = 0;

    [SerializeField] private AnimationCurve MarginPerfectEvolution;


    public void LookAt(CallbackContext callBack)
    {
        //r�cup�ration de la position de la souris par rapport � l'�cran
        MousePosition = callBack.ReadValue<Vector2>();
        

    }

    public void OnFire(CallbackContext callBack)
    {
        
        if (callBack.performed)
        {
            print(Tempo);
            //on calcul la direction entre le player et la souris 
            Vector2 DirectionShoot = Camera.main.ScreenToWorldPoint(MousePosition) - transform.position;

            //on cr�er un raycast du player dans la direction de la souris de distance max sur un mask sans le player lui-m�me
            RaycastHit2D RayShoot = Physics2D.Raycast(transform.position, DirectionShoot.normalized, float.MaxValue, TheMask);
            
            //Debug
            Debug.DrawLine(transform.position, RayShoot.point, Color.red, 0.2f);
            

            //On v�rifie si il collide avec un �l�ment et si cet �l�ment poss�de le tag ennemy
            if (RayShoot.collider != null && RayShoot.transform.tag == "Ennemy" )
            {
                //On r�cup�re le script behavior de l'ennemy touch�
                EnnemyBehavior myEnnemyScript = RayShoot.transform.GetComponent<EnnemyBehavior>();

                
                //On v�rif si le tir est dans le cadran du tir ok
                if (Tempo >= ObjectiveShoot - MarginOk && Tempo <= ObjectiveShoot + MarginOk)
                {
                    
                    //si le tir est dans le cadran tir parfait
                    if (Tempo >= ObjectiveShoot - MarginPerfect && Tempo <= ObjectiveShoot + MarginPerfect)
                    {
                        //alors on prend le script de l'ennemy touch� et on lui retire 30 pts
                        myEnnemyScript.DamageEnnemy(10 + OverHeated / 5);
                        

                        //La surchauffe augmente de 10
                        OverHeated += 10;
                        OverHeated = Mathf.Clamp(OverHeated, 0, 100);
                        print(OverHeated);
                    }
                    else
                    {
                        //Si il n'est pas dans tir parfait mais juste dans le tir ok, on prend le script de l'ennemy et on lui retire 10 pts
                        myEnnemyScript.DamageEnnemy(10);
                        //La surchauffe descend de 10
                        OverHeated -= 10;
                        OverHeated = Mathf.Clamp(OverHeated, 0, 100);
                        print(OverHeated);
                    }
                    

                }
                else
                {
                    //La surchauffe descend de 30 car le tir est rat�.
                    OverHeated -= 30;
                    OverHeated = Mathf.Clamp(OverHeated, 0, 100);
                    print(OverHeated);
                }
            }
            

        }
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

        //faire un calcul en fonction de la surchauffe et de la taille du tir pour que que se soit recalculer � chaque fois

        float ChangeValuePerfect = MarginPerfectEvolution.Evaluate(OverHeated / 100f);






    }

    public void Overheated(CallbackContext callBack)
    {
        //lorsque l'on clic sur R la surchauffe descend de 10 (celle-ci est clamper de 0 � 100)
        if (callBack.performed)
        {
            OverHeated -= 10;
            OverHeated = Mathf.Clamp(OverHeated, 0, 100);
            print(OverHeated);
        }
    }
}
