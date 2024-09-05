using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        if (!photonView.IsMine) return;

        cameraTransform.gameObject.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

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
                //playerController.TransitionToState(playerController.skillState,keyInput);
                EnterSkillState(keyInput);

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
        }

        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerController.TransitionToState(playerController.dodgeState);
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


    #region Skill State로 변경하는 RPC

    public void EnterSkillState(KeyCode keyInput)
    {
        photonView.RPC("EnterSkillState_RPC", RpcTarget.All, (int)keyInput);
    }

    [PunRPC]
    public void EnterSkillState_RPC(int keyInput)
    {
        playerController.TransitionToState(playerController.skillState, (KeyCode)keyInput);
    }

    #endregion


    #region 기본 공격

    public void Skill_Common_Attack()
    {
        if (!photonView.IsMine) return;
        if (!_isAttack) return;

        photonView.RPC("Skill_Common_Attack_RPC", RpcTarget.All);
    }

    [PunRPC]
    public void Skill_Common_Attack_RPC(PhotonMessageInfo info)
    {
        Debug.Log($"서버타임 : {info.SentServerTime}, 타임 {PhotonNetwork.Time}");

        anim.ResetTrigger(LeftMouse);
        anim.SetTrigger(LeftMouse);
    }
    #endregion




    

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 자신의 상태를 전송
            stream.SendNext(_isMove);
            stream.SendNext(_isAttack);
            stream.SendNext(_isDodge);
        }
        else
        {
            // 다른 플레이어의 상태를 수신
            _isMove = (bool)stream.ReceiveNext();
            _isAttack = (bool)stream.ReceiveNext();
            _isDodge = (bool)stream.ReceiveNext();


            // 애니메이션 동기화
            anim.SetBool(IsMove, _isMove);
            anim.SetBool(IsAttack, _isAttack);
            anim.SetBool(IsDodge, _isDodge);
        }
    }
}

