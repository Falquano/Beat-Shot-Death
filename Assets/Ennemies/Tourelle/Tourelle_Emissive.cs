using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Tourelle_Emissive : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    Rigidbody EnnemyRB;


    [SerializeField] public UnityEvent onFootstep;
    [SerializeField] private bool melee;

    [SerializeField] private Material emissiveMaterial;
    [SerializeField] private Renderer objecToChange;
    
    private Color color;
    public bool intensityOverTime;
    private float timer = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        EnnemyRB = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        emissiveMaterial = objecToChange.GetComponent<Renderer>().material;
    }

    private void Update()
    {
        //Get la velocity qui sera utile pour l'anim de déplacement
        Vector3 Velocity = agent.velocity;
        Vector3 VelocityRelative = transform.InverseTransformDirection(Velocity).normalized;

        animator.SetFloat("SpeedX", VelocityRelative.x);
        animator.SetFloat("SpeedZ", VelocityRelative.z);

        if (intensityOverTime)
        {
            timer += Time.deltaTime;
            Debug.Log("Timer =" + timer);
        }

        emissiveMaterial.SetColor("_Emission", color * timer);
    }


    public void OnHit()
    {
        if (melee)
        {
            animator.SetInteger("AttackRand", Random.Range(1, 4));
        }
        animator.SetTrigger("OnHit");
    }



    public void OnShoot()
    {
        animator.SetTrigger("OnShoot");
    }

    public void OnCharge()//Oui je ne sais pas comment on dis charger en anglais car je pense que load n'est pas approprié
    {
        animator.SetTrigger("OnShoot");
    }

    public void OnAim() //Uniquement sur les tourelle
    {
        intensityOverTime = true;

    }



    /*public void Footstep() Je sais pas pourquoi ya ça
     
    {
        onFootstep.Invoke();
    }*/


}
