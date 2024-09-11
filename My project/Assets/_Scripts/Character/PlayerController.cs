using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviourPun, Ihittable, Iattackable, IPunObservable
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

    public void Hit_Call(int damage, Iattackable attackPlayer)
    {
        status.currentHp -= damage;

        photonView.RPC("Hit_RPC", RpcTarget.Others, damage,attackPlayer.GetTransform().position);
    }

    [PunRPC]
    public void Hit_RPC(int damage, Vector3 attackPlayerPosition)
    {
        charMove.TransitionToState_Call("Hit");

        transform.LookAt(attackPlayerPosition);

        photonView.RPC("Hit_Set_DamageImmune", RpcTarget.All, true);
    }

    [PunRPC]
    public void Hit_Set_DamageImmune(bool isImmune)
    {
        charMove._damageImmune = isImmune;

        if (isImmune)
        {
            StartCoroutine(HitImmuneCoroutine());
        }
    }

    IEnumerator HitImmuneCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        photonView.RPC("Hit_Set_DamageImmune", RpcTarget.All, false);
    }


    public Transform GetTransform()
    {
        return this.transform;
    }

    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 여기서는 스탯을 업데이트할건데
        // 최대체력이나 Root 관련은 RPC를 통해 레벨업시킬 때 호출

        if (stream.IsWriting)
        {
            stream.SendNext(status.currentHp);
        }
        else
        {
            status.currentHp = (int)stream.ReceiveNext();
        }
    }

    #endregion

}
