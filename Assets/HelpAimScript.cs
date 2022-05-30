using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpAimScript : MonoBehaviour
{

    bool ObjectPointed;
    Vector3 Target;
    GameObject ObjectTarget;

    private Animator Anim;

    // Update is called once per frame
    void Update()
    {
        
        //Si un objet est pointé alors le viseur suit sa position
        if(ObjectPointed == true && ObjectTarget != null)
        {
            Target = new Vector3(ObjectTarget.transform.position.x, 6, ObjectTarget.transform.position.z);
            transform.position = Target;
        }
        else if (ObjectPointed == true && ObjectTarget == null)
        {
            this.gameObject.SetActive(false);
            ObjectPointed = false;
        }


    }

    private void OnEnable()
    {

        if(Anim == null)
        {
            Anim = GetComponent<Animator>();
        }

        Anim.SetTrigger("NewTarget");
    }

    public void OnObjectPointed(GameObject minitarget)
    {
        //Si un objet vient d'être pointé on récup sa position, on active le viseur
        ObjectTarget = minitarget;
        this.gameObject.SetActive(true);
        ObjectPointed = true;
    }

    public void OnNonObjectPointed()
    {
        //Si plus aucun objet n'est pointé, on désactive le viseur et on n'active plus sa position
        this.gameObject.SetActive(false);
        ObjectPointed = false;
    }
}
