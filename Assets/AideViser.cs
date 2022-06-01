using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AideViser : MonoBehaviour
{
    //Faire un raycast
    //Si il touche rien il renvoy une fois rien
    //Il supprime l'anim
    //Si il touche un élément il renvoy une fois cette élément
    //Il met une anim de croix rouge sur l'objet
    //Pas plus d'un élément à la fois

    [SerializeField] private ShootPlayer ScriptShootPlayer;
    [SerializeField] private LayerMask MaskAideViser;
    [SerializeField] private HelpAimScript AimScript;


    private Collider ObjectTouched;

    [SerializeField] private bool HelpAim;

    private void Update()
    {
        if (HelpAim  )
        {
            return;
        }

        Ray MouseOnScreen = Camera.main.ScreenPointToRay(ScriptShootPlayer.MouseScreenPosition);

        //Raycast de la cam à la souris sur le mask des collider aide à la visée
        if (Physics.Raycast(MouseOnScreen, out RaycastHit hitInfo, float.MaxValue, MaskAideViser))
        {
            
            //Si il touche un objet
            if (hitInfo.collider != null)
            {
                HealthSystem HealthEnnemy = hitInfo.collider.gameObject.GetComponentInParent<HealthSystem>();
                //print(hitInfo.collider.gameObject.name);
                if (HealthEnnemy.isDead == true)
                {
                    ScriptShootPlayer.Target(null);
                    AimScript.OnNonObjectPointed();
                    return;
                }
                //Si l'objet qui vient d'être touché est différent d'avant alors on change le collider et on appel la fonction qui va garder prendre l'objet pour tirer dessus
                if (ObjectTouched != hitInfo.collider)
                {
                    ObjectTouched = hitInfo.collider;
                    
                    AimScript.OnObjectPointed(hitInfo.collider.gameObject);
                    //ScriptShootPlayer.TargetRayCast = hitInfo.collider.gameObject;
                    ScriptShootPlayer.Target(ObjectTouched.gameObject);
                    
                }

               
            }
        }
        else
        {
            //Si aucun objet n'est touché
            if(ObjectTouched != hitInfo.collider)
            {

                ObjectTouched = null;
                AimScript.OnNonObjectPointed();

                //ScriptShootPlayer.TargetRayCast = null;
                ScriptShootPlayer.Target(null);
            }

            
        }

    }
}
