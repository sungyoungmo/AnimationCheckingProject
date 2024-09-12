using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerBaseState
{
    public PlayerSkillState(PlayerController _player) : base(_player)
    {

    }

    AnimatorStateInfo currentAnimState;
    int attackTime;

    public override void OnStateEnter()
    {

    }


    // input을 받아오기
    public void OnStateEnter(KeyCode input)
    {

        switch (input)
        {
            case KeyCode.Mouse0:
                attackTime = 0;

                //_player.charMove.Skill_Common_Attack();
                _player.charMove.anim.SetTrigger(_player.charMove.LeftMouse);
                
                //_player.bc.enabled = true;
                

                break;

            case KeyCode.Mouse1:
                _player.charMove.anim.SetBool(_player.charMove.IsSkill, true);
                //_player.charMove.Skill_Right_Attack();
                _player.charMove.anim.SetTrigger(_player.charMove.RightMouse);
                break;

            case KeyCode.F:

                break;
        }


        _player.charMove.SetIsAttackOn(true);
    }

    public override void OnStateExit()
    {
        _player.charMove.SetIsAttackOn(false);
        _player.charMove.anim.SetBool(_player.charMove.IsSkill, false);
        //_player.charMove.anim.SetBool(_player.charMove.IsDodge, false);
    }

    public override void OnStateUpdate()
    {
        _player.charMove.SetAnimState();
        currentAnimState = _player.charMove.anim.GetCurrentAnimatorStateInfo(0);

        //if ((_player.charMove.animStateInfo.IsTag("Attack StateMachine") ||
        //    _player.charMove.animStateInfo.IsName("Skill_RightMouse")) &&
        //    _player.charMove.animStateInfo.normalizedTime >= 0.05f &&
        //    currentAnimState.IsName(_player.charMove.animStateInfo.fullPathHash.ToString()))
        //{
        //    _player.charMove.anim.ResetTrigger(_player.charMove.LeftMouse);
        //}

        if ((_player.charMove.animStateInfo.IsTag("Attack StateMachine") ||
            _player.charMove.animStateInfo.IsName("Skill_RightMouse"))
            && 
            _player.charMove.animStateInfo.normalizedTime >= 0.99f)
        {
            //_player.charMove.TransitionToState_Call("Idle");
            _player.TransitionToState(_player.idleState);

            if (_player.charMove.anim.GetBool(_player.charMove.LeftMouse))
            {
                _player.charMove.anim.ResetTrigger(_player.charMove.LeftMouse);
            }

        }


        _player.charMove.DodgeDirectionCheck();


        if (Input.GetKeyDown(KeyCode.Mouse0) &&
            !_player.charMove.anim.GetBool(_player.charMove.LeftMouse) &&
            (_player.charMove.animStateInfo.IsTag("Attack StateMachine") && 
            _player.charMove.animStateInfo.normalizedTime <= 0.45f))
        {
            if (_player.charMove.anim.GetBool(_player.charMove.IsAttack))
            {
                _player.charMove.Skill_Common_Attack();
            }
        }

        _player.charMove.InputCheck();
    }

    public override void OnStateFixedUpdate()
    {

    }
}
