using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool locked = true;
    private bool playerWaiting = false;

    public void PlayerEnterTrigger()
    {
        if (locked)
            playerWaiting = true;
        else
            Open();
    }

    public void PlayerExitTrigger()
    {
        playerWaiting = false;
        Close();
    }

    public void Unlock()
    {
        locked = false;
        if (playerWaiting)
            Open();
    }

    public void Unlock(Zone zone) => Unlock();

    public void Open()
    {
        if (locked)
            return;

        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    public void Close()
    {
        gameObject.SetActive(true);

        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }
}
