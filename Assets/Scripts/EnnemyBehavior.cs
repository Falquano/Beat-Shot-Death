using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyBehavior : MonoBehaviour
{
    [SerializeField] private int EnnemyLife = 1000;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
