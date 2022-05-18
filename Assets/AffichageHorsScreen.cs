using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichageHorsScreen : MonoBehaviour
{
    private bool isVisible = false;

    [SerializeField] private GameObject LogoAffichage;
    private Animator LogoAnim;
    private Transform TransformAnim;

    // Start is called before the first frame update
    void Start()
    {
        LogoAnim = LogoAffichage.GetComponent<Animator>();
        TransformAnim = LogoAffichage.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isVisible)
        {
            return;
        }

        //Mettre à la bonne place de l'ennemi clamper dans l'écran
        Vector3 Target = new Vector3(transform.position.x, 10, transform.position.y); //Clamper dans l'écran
        TransformAnim.position = Target;

        //Mettre la bonne anim

    }


    void OnBecameVisible()
    {
        isVisible = true;
        print("true");
    }

    void OnBecameInvisible()
    {
        isVisible = false;
        print("False");
    }
}
