using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMove : MonoBehaviour
{
    private Vector2 Move;
    private Rigidbody myRB;

    [SerializeField] private float Speed;
    [SerializeField] private float SpeedDash;

    public bool DashIsOk = false;

    private void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    public void OnMoveInput( CallbackContext callBack)
    {
        Move = callBack.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (DashIsOk)
        {
            myRB.velocity = new Vector3(Move.x, 0, Move.y) * Speed * SpeedDash;
        }
        else
        {
            myRB.velocity = new Vector3(Move.x, 0, Move.y) * Speed;
        }
        
    }

    public float CurrentSpeed => myRB.velocity.magnitude;
}
