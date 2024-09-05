using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    public PlayerDodgeState(PlayerController _player) : base(_player)
    {

    }
    float dodgeTime;

    Vector2 dodgeDirVector = new();

    public override void OnStateEnter()
    {
        _player.charMove.anim.SetTrigger(_player.charMove.SpaceBar);

        _player.charMove.SetIsDodgeOn(true);

        _player.charMove.anim.SetBool(_player.charMove.IsDodge, true);

        if (_player.charMove.xyMoveRaw.sqrMagnitude == 0)
        {
            _player.charMove.anim.SetFloat(_player.charMove.YdirRaw, 1);
            _player.charMove.anim.SetFloat(_player.charMove.XdirRaw, 0);
        }


        dodgeTime = 0.75f;
    }

    public override void OnStateExit()
    {
        _player.charMove.SetIsDodgeOn(false);
        _player.charMove.anim.SetBool(_player.charMove.IsDodge, false);
    }

    public override void OnStateFixedUpdate()
    {

        _player.charMove.SetAnimState();

        if (_player.charMove.animStateInfo.IsName("Idle") && _player.charMove.animStateInfo.normalizedTime >= 0.05f)
        {
            _player.TransitionToState(_player.idleState);
        }
    }

    public override void OnStateUpdate()
    {
        
    }
}
