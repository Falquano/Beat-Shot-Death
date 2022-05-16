using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMove : MonoBehaviour
{
    private Vector2 Move;
    private Rigidbody RB;

    [SerializeField] private float Speed;
    [SerializeField] private float SpeedDash;
    //Speed de l'impulse du dash  
    [SerializeField] private float impulseDash;

    public bool DashIsOk = false;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    public void OnMoveInput( CallbackContext callBack)
    {
        Move = callBack.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (DashIsOk)
        {
            RB.AddForce(transform.forward * impulseDash, ForceMode.Impulse);
        }
        else
        {
            RB.velocity = new Vector3(Move.x, 0, Move.y) * Speed;
        }
        
    }


    public void OnDashImpulse()
    {
        RB.AddForce(transform.forward * impulseDash, ForceMode.Impulse);
    }


    public float CurrentSpeed => RB.velocity.magnitude;
}
