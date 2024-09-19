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
    public SkillDamagePercentage skillInfo;

    public Rigidbody rb;
    

    private void Awake()
    {
        charMove = GetComponent<CharacterInput>();
        status = GetComponent<PlayerStatus>();
        skillInfo = GetComponentInChildren<SkillDamagePercentage>();
        rb = GetComponent<Rigidbody>();

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
        TransitionToState(hitState);

        Vector3 targetPosition = new Vector3(attackPlayerPosition.x, transform.position.y, attackPlayerPosition.z);

        transform.LookAt(targetPosition);

        //rb.AddForce(transform.TransformDirection(Vector3.back) * 10, ForceMode.Impulse);


        photonView.RPC("Hit_Set_DamageImmune", RpcTarget.All, true);
    }

    [PunRPC]
    public void Hit_Set_DamageImmune(bool isImmune)
    {
        //charMove._damageImmune = isImmune;

        if (isImmune)
        {
            StartCoroutine(HitImmuneCoroutine());
        }
    }

    IEnumerator HitImmuneCoroutine()
    {
        yield return new WaitForSeconds(0.8f);
        photonView.RPC("Hit_Set_DamageImmune", RpcTarget.All, false);
    }

    public void Attack_Call()
    {
        photonView.RPC("Attack_RPC", RpcTarget.All);
    }

    [PunRPC]
    public void Attack_RPC()
    {
        playerDamageControll.EnableCollider();
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
