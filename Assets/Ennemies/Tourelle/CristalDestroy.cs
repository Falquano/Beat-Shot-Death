using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalDestroy : MonoBehaviour
{
    [SerializeField] GameObject Tourelle;
 
    

    public void DeathAnimFinish()
    {
        
        Destroy(gameObject);
        Destroy(Tourelle.GetComponent<HealthSystem>());
        Destroy(Tourelle.GetComponent<EnnemyBehavior>());
        Destroy(Tourelle.GetComponent<ShootMesure>());
        Destroy(Tourelle.GetComponent<AimMesure>());
        Destroy(Tourelle.GetComponent<EnnemySoundEffects>());
        Destroy(Tourelle.GetComponent<EnnemyAnimationEvents>());
        Destroy(Tourelle.GetComponent<Tourelle_Emissive>());
        Destroy(Tourelle.GetComponent<Animator>());

    }
}
