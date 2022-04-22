using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFader : MonoBehaviour
{
	[SerializeField] private AnimationCurve fadeCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);
	[SerializeField] private float lifetime = .2f;
	private float timer;

	private Material material;

	private void Start()
	{
		timer = 0f;
		material = GetComponentInChildren<SpriteRenderer>().material;	
	}

	private void Update()
	{
		timer += Time.deltaTime;
		material.SetFloat("_Alpha", fadeCurve.Evaluate(timer / lifetime));

		if (timer > lifetime)
			Destroy(gameObject);
	}
}
