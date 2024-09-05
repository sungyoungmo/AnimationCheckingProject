using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerBaseState
{
    public PlayerSkillState(PlayerController _player) : base(_player) 
    { 
    
    }

    public override void OnStateEnter()
    {

    }
    

    // input을 받아오기
    public void OnStateEnter(KeyCode input)
    {
        _player.charMove.SetIsAttackOn(true);


        switch (input)
        {
            case KeyCode.Mouse0:
                _player.charMove.Skill_Common_Attack();
                break;

            case KeyCode.Mouse1:
                _player.charMove.anim.SetTrigger(_player.charMove.RightMouse);
                break;

            case KeyCode.F:

                break;
        }

    }

    public override void OnStateExit()
    {
        _player.charMove.SetIsAttackOn(false);
        //_player.charMove.anim.SetBool(_player.charMove.IsDodge, false);
    }

    public override void OnStateUpdate()
    {
        _player.charMove.SetAnimState();

        if (_player.charMove.animStateInfo.IsName("Idle") && _player.charMove.animStateInfo.normalizedTime >= 0.05f)
        {
            _player.charMove.anim.ResetTrigger(_player.charMove.LeftMouse);
            _player.TransitionToState(_player.idleState);
        }

        _player.charMove.DodgeDirectionCheck();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_player.charMove.animStateInfo.IsTag("Attack StateMachine") && _player.charMove.animStateInfo.normalizedTime >= 0.1)
            {
                _player.charMove.Skill_Common_Attack();
            }
            else
            {
                return;
            }
        }

        _player.charMove.InputCheck();
    }

    public override void OnStateFixedUpdate()
    {
        
    }
}
