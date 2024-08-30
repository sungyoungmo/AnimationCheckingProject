using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerBaseState currentState;
    public PlayerIdleState idleState;
    public PlayerMoveState moveState;

    public CharacterMove charMove;


    private void Awake()
    {
        charMove = GetComponent<CharacterMove>();

        idleState = new PlayerIdleState(this);
        moveState = new PlayerMoveState(this);
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

    public void TransitionToState(PlayerBaseState newState)
    {
        //print($"{currentState}에서 {newState}로 변경됨");

        currentState.OnStateExit();
        currentState = newState;
        currentState.OnStateEnter();
    }
}
