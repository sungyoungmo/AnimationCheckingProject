using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerController _player) : base(_player)
    {

    }
    KeyCode[] skillKeys = { KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.F };

    public override void OnStateEnter()
    {
        _player.charMove.SetIsMoveOn(true);
    }

    public override void OnStateExit()
    {
        _player.charMove.SetIsMoveOn(false);

        //_player.charMove.xyMove = new Vector3(0, 0, 0);
        //_player.charMove.anim.SetFloat(_player.charMove.Xdir, 0);
        //_player.charMove.anim.SetFloat(_player.charMove.Ydir, 0);
    }

    public override void OnStateUpdate()
    {
        _player.charMove.MouseMove();

        _player.charMove.DodgeDirectionCheck();

        _player.charMove.InputCheck();

    }

    public override void OnStateFixedUpdate()
    {
        _player.charMove.PlayerMove();
    }
}
