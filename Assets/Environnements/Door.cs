using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool locked = true;
    private bool playerWaiting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (locked)
                playerWaiting = true;
            else
                Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerWaiting = false;
    }

    public void Unlock()
    {
        locked = false;
        if (playerWaiting)
            Open();
    }

    public void Unlock(Zone zone) => Unlock();

    private void Open()
    {
        Destroy(gameObject);
    }
}
