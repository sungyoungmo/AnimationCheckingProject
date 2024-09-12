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
    public PlayerDamageConroller playerDamageControll;
    

    private void Awake()
    {
        charMove = GetComponent<CharacterInput>();
        status = GetComponent<PlayerStatus>();

        idleState = new PlayerIdleState(this);
        moveState = new PlayerMoveState(this);
        skillState = new PlayerSkillState(this);
        dodgeState = new PlayerDodgeState(this);
        hitState = new PlayerHitState(this);

        //skillInfo = playerDamageControll.gameObject.GetComponent<SkillDamagePercentage>();
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


    #region TransitionToState �Լ�
    public void TransitionToState(PlayerBaseState newState)
    {
        //Debug.Log($"���� ����: {currentState}, ���ο� ����: {newState}");


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

    #region Hit and Attack �Լ�

    public void Hit_Call(int damage, Iattackable attackPlayer)
    {
        status.currentHp -= damage;

        photonView.RPC("Hit_RPC", RpcTarget.Others, damage,attackPlayer.GetTransform().position);
    }

    [PunRPC]
    public void Hit_RPC(int damage, Vector3 attackPlayerPosition)
    {
        //charMove.TransitionToState_Call("Hit");
        TransitionToState(hitState);

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
    #endregion

    /*
        ���⿡ ��ų ������ �ϴ� �Լ��� ȣ���ϴ� RPC ����
        ���������� ��ų �������� �ϴ� �ڵ�� SkillDamagePercentage��
    */

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // ���⼭�� ������ ������Ʈ�Ұǵ�
        // �ִ�ü���̳� Root ������ RPC�� ���� ��������ų �� ȣ��

        if (stream.IsWriting)
        {
            stream.SendNext(status.currentHp);
        }
        else
        {
            status.currentHp = (int)stream.ReceiveNext();
        }
    }

    

}
