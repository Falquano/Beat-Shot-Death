using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMove : MonoBehaviour
{
    private Vector2 Move;
    private Rigidbody2D myRB;

    [SerializeField] private float Speed;

    public void OnMoveInput( CallbackContext callBack)
    {
        Move = callBack.ReadValue<Vector2>();
    }



    private void Update()
    {
        myRB = GetComponent<Rigidbody2D>();
        myRB.velocity = Move * Speed;
    }
}
