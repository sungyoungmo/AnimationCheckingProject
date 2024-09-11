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
        
    }
    public override void OnStateFixedUpdate()
    {
        
    }
}
