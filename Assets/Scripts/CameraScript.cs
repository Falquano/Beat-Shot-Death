using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject monplayer;
    Transform myCameraTransform;
    Transform myPlayerTransform;

    private Vector3 myTarget;
    Vector3 Velocity;


    // Start is called before the first frame update
    void Start()
    {
        myPlayerTransform = monplayer.GetComponent<Transform>();
        myCameraTransform = GetComponent<Transform>();
        Velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        myTarget = new Vector3(myPlayerTransform.position.x, myPlayerTransform.position.y, myPlayerTransform.position.z - 10);
        myCameraTransform.position = Vector3.SmoothDamp(myCameraTransform.position, myTarget, ref Velocity, 0.1F);
        
    }
}
