using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool locked = true;
    private bool playerWaiting = false;
    
    public Animation anim;
    [SerializeField] private Material close;
    [SerializeField] private Material open;
    [SerializeField] private GameObject change1;
    [SerializeField] private GameObject change2;
    [SerializeField] private StudioEventEmitter closeEmitter;

    private void Start()
    {
        GetComponent<Collider>().enabled = false;
    }
    
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
        change1.GetComponent<MeshRenderer>().material = open;
        change2.GetComponent<MeshRenderer>().material = open;
        if (playerWaiting)
            Open();
    }

    public void Unlock(Zone zone) => Unlock();

    public void Open()
    {
        /*if (locked)
            return;
        */
        
        if(!locked)
            Debug.Log("OpeningNow");
        //GetComponent<Renderer>().enabled = false;

        GetComponent<Collider>().enabled = false;
            anim.Play("Open");
    }

    public void Close()
    {
        gameObject.SetActive(true);
        anim.Play("Idle_Close");
        //GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;

    }
}
