using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Zone : MonoBehaviour
{
	[SerializeField] public string Name = "Untitled Zone";
	[SerializeField] private EnnemyBehavior[] ennemies;
	[SerializeField] public UnityEvent onPlayerEnter;

	private int aliveEnnemies;

	private void Start()
	{
		aliveEnnemies = ennemies.Length;
		
		foreach(EnnemyBehavior ennemy in ennemies)
		{
			ennemy.GetComponent<HealthSystem>().onDie.AddListener(EnnemyDies);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			ActivateEnnemies();
			onPlayerEnter.Invoke();
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
			Debug.Log($"{name} cleared.");
		}
	}
}