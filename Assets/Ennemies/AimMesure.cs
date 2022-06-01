using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;




public class AimMesure : Mesure
{
    public float rotationSpeed;
    [SerializeField][Range(0f, 1f)] private float aimingTime = .5f;

    [SerializeField] public UnityEvent onAim;
    [SerializeField] public float distanceMaxWithPlayer;

    [SerializeField] private LineRenderer viseuwrLasewr;

    [SerializeField] private float range = 100f;
    [SerializeField] LayerMask EnnemyMask;
    [SerializeField] private float rayOffset = .7f;

    private void OnEnable()
    {
        
        onAim.Invoke();

        viseuwrLasewr.enabled = true;
    }

    private void OnDisable()
    {
        viseuwrLasewr.enabled = false;
    }

    private void Update()
    {
        if (behavior.Player == null || PlayerisDead == true)
            return;


        // Mise à jour du viseur laser
        Ray ray = new Ray(transform.position + transform.forward * rayOffset + new Vector3(0, 2, 0), transform.forward);

        viseuwrLasewr.SetPosition(0, viseuwrLasewr.transform.position);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, range, EnnemyMask))
        {
            viseuwrLasewr.SetPosition(1, hitInfo.point);
        }

        if (tempo.Tempo > aimingTime)
            return;


        //Si l'ennemy est trop loin du player il ne vise pas, donc ne fait pas de trait vfx rouge
        if (Vector3.Distance(transform.position, Player.transform.position) > DistanceMaxShoot)
        {
            return;
        }

        //R�cup�ration du transform du player
        Transform playerTransform = behavior.Player.GetComponent<Transform>();


        //Calcul du vector entre l'ennemi et le player
        Vector3 direction = playerTransform.position - transform.position;
        //Le freeze du rigidbody ne fonctionne pas
        direction = new Vector3(direction.x, 0, direction.z);

        //L'ennemi regarde le player
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);



    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, DistanceMaxShoot);
    }

}
