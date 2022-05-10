using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnnemyWaves : MonoBehaviour
{
    [SerializeField]
    private GameObject ennemyMelee;


    [SerializeField] private float timer;
    [SerializeField] private float timeToSpawn;
    public Transform[] spawnPoint;
    private int randomSpawn;
    [SerializeField] private EnnemyBehavior[] ennemiesArray;
    public int livingEnnemies;
    [SerializeField] private int totalEnnemies;
    [SerializeField] private int wave;
    [SerializeField] private bool close;
    [SerializeField] private int varEnnemiMax;
    private Door[] doors;

    // Start is called before the first frame update
    void Start()
    {
        doors = GetComponentsInChildren<Door>();
        ennemiesArray = GetComponentsInChildren<EnnemyBehavior>();
        livingEnnemies = ennemiesArray.Length;
        totalEnnemies = ennemiesArray.Length;

        close = false;

        foreach (EnnemyBehavior ennemy in ennemiesArray)
        {
            ennemy.GetComponent<HealthSystem>().onDie.AddListener(EnnemyDies);
        }
    }

    private void EnnemyDies()
    {
        livingEnnemies--;

    }

        // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(!close)
        {
            if (timer >= timeToSpawn && livingEnnemies < varEnnemiMax && totalEnnemies < wave)
            {
                randomSpawn = Random.Range(0, spawnPoint.Length);

                EnnemyBehavior behavior = Instantiate(ennemyMelee, spawnPoint[randomSpawn].transform.position, Quaternion.identity)
                    .GetComponent<EnnemyBehavior>();
                
                behavior.Active = true;
                
                timer = 0f;
                ennemiesArray = GetComponentsInChildren<EnnemyBehavior>();
                livingEnnemies = ennemiesArray.Length;
                totalEnnemies++;


                if(totalEnnemies >= wave)
                {
                    DoorClose();
                }
            }
        }


    }

    private void DoorClose()
    {
        if (close)
            return;

        close = true;
        foreach(Door door in doors)
        {
            door.Close();
        }
    }
}
