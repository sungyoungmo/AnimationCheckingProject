using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviourPun, IPunObservable
{
    public readonly int Xdir = Animator.StringToHash("Xdir");
    public readonly int Ydir = Animator.StringToHash("Ydir");
    public readonly int IsMove = Animator.StringToHash("IsMove");

    Vector2 xyMove = new Vector2();
    Animator anim;

    public float mouseYMinAngle = -35f;
    public float mouseYMaxAngle = 35f;

    public Transform cameraTransform;
    public Transform lookingSpot;

    public float mouseSensitivity = 3f;

    private float mouseXAxis;
    private float rotationZ = 0f;

    public bool _isMove;

    private void Awake()
    {
        if (!photonView.IsMine) return;

        cameraTransform.gameObject.SetActive(true);

        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        anim = GetComponent<Animator>();
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

    public void SetIsMoveOn(bool nowMove)
    {
        _isMove = nowMove;
        anim.SetBool(IsMove, nowMove);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
