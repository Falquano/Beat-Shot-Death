using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    [SerializeField] private ShootPlayer player;
    private Material material;

    private void Start()
    {
        material = GetComponent<Image>().material;
    }

    private void Update()
    {
        transform.position = player.MousePosition;
        material.SetFloat("tempo", player.Tempo);
    }
}
