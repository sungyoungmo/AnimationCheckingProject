using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerController _player) : base(_player) 
    { 
    
    }

    float playerXMove;
    float playerYMove;

    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        _player.charMove.MouseMove();

        playerXMove = Input.GetAxisRaw("Horizontal");
        playerYMove = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D))
        {
            _player.TransitionToState(_player.moveState);
        }

    }

    public override void OnStateFixedUpdate()
    {
        
    }
}
