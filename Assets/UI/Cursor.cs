using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    [SerializeField] private ShootPlayer player;
    private Material material;

    public float TargetValue { get => material.GetFloat("targetValue"); set => material.SetFloat("targetValue", value); }
    public float PerfectMargin { get => material.GetFloat("perfectMargin"); set => material.SetFloat("perfectMargin", value); }
    public float OkayMargin { get => material.GetFloat("okMargin"); set => material.SetFloat("okMargin", value); }

    private void Start()
    {
        material = GetComponent<Image>().material;
    }

    private void Update()
    {
        transform.position = player.MousePosition;
        material.SetFloat("tempo", player.Tempo);
    }

    private void OnDestroy()
    {
        material.SetFloat("tempo", 0);
    }
}
