using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    public PlayerDodgeState(PlayerController _player) : base(_player)
    {

    }

    float dodgeTime;

    public override void OnStateEnter()
    {
        _player.charMove.anim.SetTrigger(_player.charMove.SpaceBar);
        _player.charMove.anim.SetBool(_player.charMove.IsDodge, true);
        dodgeTime = 1;
    }

    public override void OnStateExit()
    {
        _player.charMove.anim.SetBool(_player.charMove.IsDodge, false);
    }

    public override void OnStateFixedUpdate()
    {
        dodgeTime -= Time.deltaTime;
        if (dodgeTime < 0)
        {
            _player.TransitionToState(_player.idleState);
        }
    }

    public override void OnStateUpdate()
    {
        
    }
}
