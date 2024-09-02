using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerController _player) : base(_player)
    {

    }
    KeyCode[] skillKeys = { KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.Space, KeyCode.F };

    public override void OnStateEnter()
    {
        _player.charMove.SetIsMoveOn(true);
    }

    public override void OnStateExit()
    {
        _player.charMove.SetIsMoveOn(false);
    }

    public override void OnStateUpdate()
    {
        _player.charMove.MouseMove();
        
        // IdleState·Î
        if (!(
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D)))
        {
            _player.TransitionToState(_player.idleState);
        }

        // SkillState·Î
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
        _player.charMove.PlayerMove();
    }
}
