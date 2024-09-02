using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerBaseState
{
    public PlayerSkillState(PlayerController _player) : base(_player) 
    { 
    
    }

    float battleTime;

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
                _player.charMove.anim.SetTrigger(_player.charMove.LeftMouse);
                break;

            case KeyCode.Mouse1:
                _player.charMove.anim.SetTrigger(_player.charMove.RightMouse);
                //battleTime = 3f;
                break;

            case KeyCode.Space:
                _player.charMove.anim.SetTrigger(_player.charMove.SpaceBar);
                _player.charMove.anim.SetBool(_player.charMove.IsDodge, true);
                //battleTime = 1f;
                break;

            case KeyCode.F:

                break;
        }

        battleTime = 0;
    }

    public override void OnStateExit()
    {
        _player.charMove.SetIsAttackOn(false);
        _player.charMove.anim.SetBool(_player.charMove.IsDodge, false);
    }

    public override void OnStateUpdate()
    {
        battleTime += Time.deltaTime;

        if (battleTime > 1.0f)
        {
            _player.TransitionToState(_player.idleState);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _player.charMove.anim.SetTrigger(_player.charMove.LeftMouse);

            battleTime = 0;
        }
    }

    public override void OnStateFixedUpdate()
    {
        
    }
}
