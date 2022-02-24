using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyBehavior : MonoBehaviour
{
    [SerializeField] private int EnnemyLife = 1000;

    [SerializeField] private GameObject BulletGO;
    [SerializeField] private GameObject SpawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject bulletEnnemy = Instantiate(BulletGO, SpawnPoint.transform.position, SpawnPoint.transform.rotation, SpawnPoint.transform);
    }

    public void DamageEnnemy(int HitLife)
    {
        EnnemyLife -= HitLife;
        
        if(EnnemyLife <= 0)
        {
            Destroy(this.gameObject);
        }

    }
}
