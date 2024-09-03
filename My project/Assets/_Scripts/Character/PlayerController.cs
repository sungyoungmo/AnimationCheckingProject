using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerBaseState currentState { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerSkillState skillState { get; private set; }
    public PlayerDodgeState dodgeState { get; private set; }
    public PlayerHitState hitState { get; private set; }
    

    public CharacterInput charMove;

    

    private void Awake()
    {
        charMove = GetComponent<CharacterInput>();

        idleState = new PlayerIdleState(this);
        moveState = new PlayerMoveState(this);
        skillState = new PlayerSkillState(this);
        dodgeState = new PlayerDodgeState(this);
        hitState = new PlayerHitState(this);
    }

    private void OnEnable()
    {
        currentState = idleState;
        idleState.OnStateEnter();
    }

    private void Update()
    {
        currentState.OnStateUpdate();
    }

    private void FixedUpdate()
    {
        currentState.OnStateFixedUpdate();
    }

    public void TransitionToState(PlayerBaseState newState)
    {
        currentState.OnStateExit();
        currentState = newState;
        currentState.OnStateEnter();
    }

    public void TransitionToState(PlayerBaseState newState, KeyCode input)
    {
        currentState.OnStateExit();
        currentState = newState;
        skillState.OnStateEnter(input);
    }
}
