using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerBaseState currentState { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerSkillState skillState { get; private set; }

    public CharacterInput charMove;


    private void Awake()
    {
        charMove = GetComponent<CharacterInput>();

        idleState = new PlayerIdleState(this);
        moveState = new PlayerMoveState(this);
        skillState = new PlayerSkillState(this);
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
        //print($"{currentState}에서 {newState}로 변경됨");

        currentState.OnStateExit();
        currentState = newState;
        currentState.OnStateEnter();
    }

    public void TransitionToSkillState(KeyCode input)
    {
        //print($"{currentState}에서 {newState}로 변경됨");

        currentState.OnStateExit();
        currentState = skillState;
        skillState.OnStateEnter(input);
    }
}
