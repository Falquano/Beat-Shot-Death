using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMove : MonoBehaviour
{
    private Vector2 Move;
    private Rigidbody myRB;

    [SerializeField] private float Speed;

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
        myRB.velocity = new Vector3(Move.x, 0, Move.y) * Speed;
    }

    public float CurrentSpeed => myRB.velocity.magnitude;
}
