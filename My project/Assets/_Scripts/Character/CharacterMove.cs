using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviourPun, IPunObservable
{
    readonly int Xdir = Animator.StringToHash("Xdir");
    readonly int Ydir = Animator.StringToHash("Ydir");
    
    
    Vector2 xyMove = new Vector2();
    Animator anim;


    public float mouseYMinAngle = -35f;
    public float mouseYMaxAngle = 35f;

    public Transform cameraTransform;
    public Transform lookingSpot;
    private float speed = 3f;

    private float mouseXAxis;
    private float rotationZ = 0f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        if (!photonView.IsMine) return;

        cameraTransform.gameObject.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        if (!photonView.IsMine) return;

        PlayerMove();

        MouseMove();
    }

    private void PlayerMove()
    {
        xyMove.x = Input.GetAxis("Horizontal");
        xyMove.y = Input.GetAxis("Vertical");

        anim.SetFloat(Xdir, xyMove.x);
        anim.SetFloat(Ydir, xyMove.y);
    }

    void MouseMove()
    {
        mouseXAxis = Input.GetAxis("Mouse X") * speed;

        rotationZ += Input.GetAxisRaw("Mouse Y") * speed;

        rotationZ = Mathf.Clamp(rotationZ, mouseYMinAngle, mouseYMaxAngle);

        transform.Rotate(0f, mouseXAxis, 0f, Space.World);

        lookingSpot.localEulerAngles = new Vector3(0, -90, rotationZ);

        cameraTransform.transform.LookAt(lookingSpot.position);

    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
