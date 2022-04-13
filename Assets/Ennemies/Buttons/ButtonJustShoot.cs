using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonJustShoot : MonoBehaviour
{
    [SerializeField] Animator ButtonAnim;
    [SerializeField] GameObject Door;

    // Start is called before the first frame update
    void Start()
    {
        ButtonAnim = GetComponent<Animator>();
    }

    public void DoorOpening()
    {
        Door.SetActive(false);
    }
}
