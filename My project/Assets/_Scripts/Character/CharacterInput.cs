using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterInput : MonoBehaviourPun, IPunObservable
{
    public readonly int Xdir = Animator.StringToHash("Xdir");
    public readonly int Ydir = Animator.StringToHash("Ydir");
    public readonly int IsMove = Animator.StringToHash("IsMove");
    public readonly int LeftMouse = Animator.StringToHash("LeftMouse");
    public readonly int IsAttack = Animator.StringToHash("IsAttack");
    public readonly int RightMouse = Animator.StringToHash("RightMouse");
    public readonly int SpaceBar = Animator.StringToHash("SpaceBar");
    public readonly int IsDodge = Animator.StringToHash("IsDodge");
    public readonly int XdirRaw = Animator.StringToHash("XdirRaw");
    public readonly int YdirRaw = Animator.StringToHash("YdirRaw");
    public readonly int Hit = Animator.StringToHash("Hit");
    public readonly int IsSkill = Animator.StringToHash("IsSkill");

    KeyCode[] skillKeys = { KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.F };

    public AnimatorStateInfo animStateInfo;


    public Vector2 xyMove = new Vector2();
    public Vector2 xyMoveRaw = new Vector2();

    public Animator anim;
    public PlayerController playerController;

    public float mouseYMinAngle = -35f;
    public float mouseYMaxAngle = 35f;

    public Transform cameraTransform;
    public Transform lookingSpot;

    public float mouseSensitivity = 3f;

    private float mouseXAxis;
    private float rotationZ = 0f;

    public bool _isMove;
    public bool _isAttack;
    public bool _isDodge;

    public bool _damageImmune;

    private void Awake()
    {
        if (!photonView.IsMine) return;

        cameraTransform.gameObject.SetActive(true);
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


        // 직접 인스페터에서 할당 해놨음
        //anim = GetComponent<Animator>();
        //playerController = GetComponent<PlayerController>();
    }
    private void Start()
    {

    }

    public void PlayerMove()
    {
        if (!photonView.IsMine) return;

        xyMove.x = Input.GetAxis("Horizontal");
        xyMove.y = Input.GetAxis("Vertical");


        anim.SetFloat(Xdir, xyMove.x);
        anim.SetFloat(Ydir, xyMove.y);

    }

    public void DodgeDirectionCheck()
    {
        if (!photonView.IsMine) return;

        xyMoveRaw.x = Input.GetAxisRaw("Horizontal");
        xyMoveRaw.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat(XdirRaw, xyMoveRaw.x);
        anim.SetFloat(YdirRaw, xyMoveRaw.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerController.TransitionToState(playerController.dodgeState);
            //TransitionToState_Call("Dodge");
        }
    }

    public void MouseMove()
    {
        if (!photonView.IsMine) return;

        mouseXAxis = Input.GetAxis("Mouse X") * mouseSensitivity;

        rotationZ += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        rotationZ = Mathf.Clamp(rotationZ, mouseYMinAngle, mouseYMaxAngle);

        transform.Rotate(0f, mouseXAxis, 0f, Space.World);

        lookingSpot.localEulerAngles = new Vector3(0, -90, rotationZ);

        cameraTransform.transform.LookAt(lookingSpot.position);
    }

    public void AttackCheck()
    {
        if (!photonView.IsMine) return;

        foreach (var keyInput in skillKeys)
        {
            if (!_isAttack && Input.GetKeyDown(keyInput))
            {
                playerController.TransitionToState(playerController.skillState,keyInput);
                break;
            }
        }
    }

    public void InputCheck()
    {
        if (!photonView.IsMine) return;

        if (!_isMove && !_isAttack &&
            (
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D)))
        {
            playerController.TransitionToState(playerController.moveState);
            //TransitionToState_Call("Move");
        }
        else if 
            (_isMove && !_isAttack &&
            !(
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D)))
        {
            playerController.TransitionToState(playerController.idleState);
            //TransitionToState_Call("Idle");
        }

        

        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.visible)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void SetAnimState()
    {
        animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
    }

    #region SetStateOn
    public void SetIsMoveOn(bool nowMove)
    {
        _isMove = nowMove;
        anim.SetBool(IsMove, nowMove);
    }

    public void SetIsAttackOn(bool nowAttack)
    {
        _isAttack = nowAttack;
        anim.SetBool(IsAttack, nowAttack);
    }

    public void SetIsDodgeOn(bool nowDodge)
    {
        _isDodge = nowDodge;
        anim.SetBool(IsDodge, nowDodge);
    }

    #endregion


    #region 상태 변경 RPC

    //public void TransitionToState_Call(string newState)
    //{
    //    if (!photonView.IsMine) return;
    //    photonView.RPC("TransitionToState_Call_RPC", RpcTarget.AllBuffered, newState);
    //}

    //[PunRPC]
    //public void TransitionToState_Call_RPC(string newState)
    //{
    //    anim.ResetTrigger(LeftMouse);
    //    anim.ResetTrigger(RightMouse);

    //    switch (newState)
    //    {
    //        case "Idle":
    //            playerController.TransitionToState(playerController.idleState);
    //            break;

    //        case "Move":
    //            playerController.TransitionToState(playerController.moveState);
    //            break;

    //        case "Skill":
    //            playerController.TransitionToState(playerController.skillState);
    //            break;

    //        case "Dodge":
    //            playerController.TransitionToState(playerController.dodgeState);
    //            break;

    //        case "Hit":
    //            playerController.TransitionToState(playerController.hitState);
    //            break;
    //    }
    //}

    #endregion

    #region 기본 공격

    public void Skill_Common_Attack()
    {
        if (!photonView.IsMine) return;
        photonView.RPC("Skill_Common_Attack_RPC", RpcTarget.AllBuffered, photonView.ViewID);
    }

    [PunRPC]
    public void Skill_Common_Attack_RPC(int attackerViewID)
    {
        if (photonView.ViewID != attackerViewID) return;

        anim.SetTrigger(LeftMouse);
    }
    #endregion

    #region 스킬 공격
    public void Skill_Right_Attack()
    {
        if (!photonView.IsMine) return;

        photonView.RPC("Skill_Right_Attack_RPC", RpcTarget.AllBuffered, photonView.ViewID);
    }

    [PunRPC]
    public void Skill_Right_Attack_RPC(int attackerViewID)
    {
        if (photonView.ViewID != attackerViewID) return;

        anim.SetTrigger(RightMouse);
    }

    #endregion




    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_isAttack);
        }
        else
        {
            bool isOnAttack = (bool)stream.ReceiveNext();

            anim.SetBool(IsAttack, isOnAttack);
        }
    }
}