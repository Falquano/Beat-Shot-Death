using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ShootPlayer : MonoBehaviour
{
    private Vector2 mousePosition;
    [SerializeField] Camera cam;

    

    public void LookAt(CallbackContext callBack)
    {
        mousePosition = callBack.ReadValue<Vector2>();
        mousePosition = cam.ScreenToWorldPoint(mousePosition);

    }


    // Update is called once per frame
    void Update()
    {
        float AngleRad = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

        print(mousePosition);
        Debug.DrawRay(transform.position, mousePosition * 3, Color.red);
    }
}
