using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerController _player) : base(_player) 
    { 
    
    }
    KeyCode[] skillKeys = { KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.Space, KeyCode.F };

    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        _player.charMove.MouseMove();


        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D))
        {
            _player.TransitionToState(_player.moveState);
        }

        //if (Input.GetMouseButtonDown(0) ||
        //    Input.GetMouseButtonDown(1) ||
        //    Input.GetKeyDown(KeyCode.Space) ||
        //    Input.GetKeyDown(KeyCode.F)
        //    )
        //{
        //    _player.TransitionToState(_player.skillState);
        //}

        foreach (var keyInput in skillKeys)
        {
            if (Input.GetKeyDown(keyInput))
            {
                _player.TransitionToSkillState(keyInput);
                break;
            }
        }
    }

    public override void OnStateFixedUpdate()
    {
        
    }
}
