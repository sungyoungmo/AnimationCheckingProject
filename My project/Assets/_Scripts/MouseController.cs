using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Transform player;
    public Transform lookingSpot;
    private float speed = 3f;

    public float mouseYMinAngle = -35f;
    public float mouseYMaxAngle = 35f;

    private float mouseXAxis;
    private float rotationZ = 0f;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MouseMove();
    }

    void MouseMove()
    {
        mouseXAxis = Input.GetAxis("Mouse X") * speed;

        rotationZ -= Input.GetAxis("Mouse Y") * speed;

        rotationZ = Mathf.Clamp(rotationZ, mouseYMinAngle, mouseYMaxAngle);

        player.transform.Rotate(0f, mouseXAxis, 0f, Space.World);

        lookingSpot.localEulerAngles = new Vector3(0, -90, rotationZ);

        transform.LookAt(lookingSpot.position);
    }
}
