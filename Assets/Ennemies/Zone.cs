using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Zone : MonoBehaviour
{
	[SerializeField] public string Name = "Untitled Zone";
	[SerializeField] private EnnemyBehavior[] ennemies;
	[SerializeField] public UnityEvent<Zone> onPlayerEnter;
	[SerializeField] public UnityEvent<Zone> onRoomCleared;



	private int aliveEnnemies;

	private void Start()
	{
		ennemies = GetComponentsInChildren<EnnemyBehavior>();
		aliveEnnemies = ennemies.Length;


		foreach(EnnemyBehavior ennemy in ennemies)
		{
			ennemy.GetComponent<HealthSystem>().onDie.AddListener(EnnemyDies);
		}

		foreach(Door door in GetComponentsInChildren<Door>())
        {
			if (aliveEnnemies > 0)
				onRoomCleared.AddListener(door.Unlock);
			else
				door.Unlock();
        }
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			ActivateEnnemies();
			
			onPlayerEnter.Invoke(this);
		}
	}

	private void ActivateEnnemies()
	{
		foreach(EnnemyBehavior ennemy in ennemies)
		{
			ennemy.ActivateAtNextMesure();
		}
	}

	private void EnnemyDies()
	{
		aliveEnnemies--;

		if (aliveEnnemies <= 0)
		{
			
			onRoomCleared.Invoke(this);
		}
	}
}
