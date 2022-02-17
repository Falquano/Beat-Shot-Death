using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject monplayer;
    Transform myCameraTransform;
    Transform myPlayerTransform;


    // Start is called before the first frame update
    void Start()
    {
        myPlayerTransform = monplayer.GetComponent<Transform>();
        myCameraTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        myCameraTransform.position = new Vector3(myPlayerTransform.position.x, myPlayerTransform.position.y, myPlayerTransform.position.z - 10);
    }
}
