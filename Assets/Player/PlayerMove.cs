using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMove : MonoBehaviour
{
    private Vector2 Move;
    private Rigidbody RB;

    [SerializeField] private float Speed;
    //Speed de l'impulse du dash  
    [SerializeField] private float impulseDash;

    public bool DashIsOk = false;
    [SerializeField] private PlayerHealthSystem ScriptHealthPlayer;

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
        if (ScriptHealthPlayer.PlayerisDead)
        {
            RB.velocity = Vector3.zero;
            
            return;
        }

        if (DashIsOk )
        {
            RB.velocity = Vector3.zero;
            //Mettre le déplacement sur le rigidbody met les collisions en jeu donc c'est cool
            RB.velocity = transform.forward * impulseDash;
        }
        else
        {
            RB.velocity = new Vector3(Move.x, 0, Move.y) * Speed;
        }
        
    }


   public void OnDashVelocityZero()
    {
        
        RB.velocity = Vector3.zero;
    }

    public float CurrentSpeed => RB.velocity.magnitude;
}
