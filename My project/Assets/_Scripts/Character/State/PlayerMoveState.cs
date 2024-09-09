using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerController _player) : base(_player)
    {

    }

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

        _player.charMove.DodgeDirectionCheck();

        _player.charMove.AttackCheck();

        _player.charMove.InputCheck();

    }

    public override void OnStateFixedUpdate()
    {
        _player.charMove.PlayerMove();
    }
}