using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnnemyWaves : MonoBehaviour
{
    [SerializeField]
    private GameObject[] ennemy;


    [SerializeField] private float timer;
    [SerializeField] private float timeToSpawn;
    public List<Transform> spawnPoints;
    private int randomSpawn;
    private int randomEnnemy;
    [SerializeField] private EnnemyBehavior[] ennemiesArray;
    public int livingEnnemies;
    [SerializeField] private int totalEnnemies;
    [SerializeField] private int wave;
    public bool close;
    [SerializeField] private int varEnnemiMax;
    private Door[] doors;
    public Collider exitCollider;
    public Collider EnterCollider;
    private GarbageCollector[] triselectif;
    // Start is called before the first frame update
    void Start()
    {
        doors = GetComponentsInChildren<Door>();
        ennemiesArray = GetComponentsInChildren<EnnemyBehavior>();
        livingEnnemies = ennemiesArray.Length;
        totalEnnemies = ennemiesArray.Length;
        triselectif = GetComponentsInChildren<GarbageCollector>();

        close = true;

        foreach (EnnemyBehavior ennemy in ennemiesArray)
        {
            ennemy.GetComponent<HealthSystem>().onDie.AddListener(EnnemyDies);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            close = false;
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            close = true;
           
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
                randomSpawn = Random.Range(0, spawnPoints.Count);
                randomEnnemy = Random.Range(0, ennemy.Length);

                EnnemyBehavior behavior = Instantiate(ennemy[randomEnnemy], spawnPoints[randomSpawn].transform.position, Quaternion.identity)
                    .GetComponent<EnnemyBehavior>();
                behavior.GetComponent<HealthSystem>().onDie.AddListener(EnnemyDies);

                behavior.Active = true;
                
                timer = 0f;
                ennemiesArray = GetComponentsInChildren<EnnemyBehavior>();
                livingEnnemies++;
                totalEnnemies++;


                if(totalEnnemies >= wave)
                {
                    CloseAllDoors();
                }
            }
        }


    }

    public void DisposeSpawn(Transform spawner)
    {
        spawnPoints.Remove(spawner);
        if(spawnPoints.Count == 0)
        {
            CloseAllDoors();
        }
    }

    private void CloseAllDoors()
    {
        if (close)
            return;

        close = true;
        foreach(Door door in doors)
        {
            door.Close();
        }

        foreach (GarbageCollector zozo in triselectif)
        {
            zozo.TriSelectif();
        }
    }
}
