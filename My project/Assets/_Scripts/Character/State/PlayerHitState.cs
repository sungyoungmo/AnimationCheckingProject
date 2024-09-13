using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : PlayerBaseState
{
    public PlayerHitState(PlayerController _player) : base(_player)
    {

    }

    public override void OnStateEnter()
    {
        _player.charMove.anim.SetTrigger(_player.charMove.Hit);
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        _player.charMove.SetAnimState();

        
        if (_player.charMove.animStateInfo.IsName("Hit") && _player && _player.charMove.animStateInfo.normalizedTime >= 0.80f)
        {
            _player.TransitionToState(_player.idleState);
        }

        _player.charMove.DodgeDirectionCheck();
    }
    public override void OnStateFixedUpdate()
    {
        
    }
}
