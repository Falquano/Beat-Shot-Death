using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEditor.VFX;
using UnityEditor.VFX.UI;

public class FuturLineActivation : MonoBehaviour
{
    public VisualEffect FuturLine;
    private ShootMesure Vise;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FuturLine.Play();
        //if()
        {

            //FuturLine.Play();

        }
        //if(! OnShoot)
        {
            //rien ne se passe ou 
            //si elle spawn encore 
            //FuturLine.Stop();

        }
        
    }
}
