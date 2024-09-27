using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ngPlayerController : MonoBehaviour
{
    public ngPlayerStatus status;

    public Canvas userCanvas;

    public Animator anim;

    private void Awake()
    {
        status = GetComponent<ngPlayerStatus>();

        anim = GetComponent<Animator>();
    }
}
