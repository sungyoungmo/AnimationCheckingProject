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

    public Vector2 xyMove = new Vector2();
    public Vector2 xyMoveRaw = new Vector2();

    public Animator anim;
    PlayerController playerController;

    public float mouseYMinAngle = -35f;
    public float mouseYMaxAngle = 35f;

    public Transform cameraTransform;
    public Transform lookingSpot;

    public float mouseSensitivity = 3f;

    private float mouseXAxis;
    private float rotationZ = 0f;

    public bool _isMove;
    public bool _isAttack;

    private void Awake()
    {
        if (!photonView.IsMine) return;

        cameraTransform.gameObject.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
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

    public void InputCheck()
    {
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

        foreach (var keyInput in skillKeys)
        {
            if (Input.GetKeyDown(keyInput))
            {
                playerController.TransitionToState(playerController.skillState,keyInput);
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerController.TransitionToState(playerController.dodgeState);
        }
    }

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


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
