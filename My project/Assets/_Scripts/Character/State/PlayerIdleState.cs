using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerController _player) : base(_player) 
    { 
    
    }
    

    public override void OnStateEnter()
    {

    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        _player.charMove.MouseMove();

        _player.charMove.AttackCheck();

        _player.charMove.InputCheck();

        _player.charMove.DodgeDirectionCheck();
    }

    public override void OnStateFixedUpdate()
    {
        
    }
}
