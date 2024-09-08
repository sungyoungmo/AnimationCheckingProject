using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour, Ihittable, Iattackable
{
    #region States
    public PlayerBaseState currentState { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerSkillState skillState { get; private set; }
    public PlayerDodgeState dodgeState { get; private set; }
    public PlayerHitState hitState { get; private set; }
    
    #endregion

    public CharacterInput charMove;
    public PlayerStatus status;
    

    private void Awake()
    {
        charMove = GetComponent<CharacterInput>();
        status = GetComponent<PlayerStatus>();

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


    #region TransitionToState 함수
    public void TransitionToState(PlayerBaseState newState)
    {
        //Debug.Log($"현재 상태: {currentState}, 새로운 상태: {newState}");


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
    #endregion

    #region Hit and Attack 함수

    public void Hit(int damage)
    {

    }
    public void Hit(int damage, Iattackable attackPlayer)
    {
        Transform attackPlayerTransform = attackPlayer.GetTransform();

        transform.LookAt(attackPlayerTransform);
    }

    public Transform GetTransform()
    {
        return this.transform;
    }
    #endregion

    

}
