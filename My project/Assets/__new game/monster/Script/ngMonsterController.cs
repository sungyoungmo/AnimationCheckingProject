using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ngMonsterController : MonoBehaviourPun, IPunObservable
{
    public ngMonster monsterInfo;

    public GameObject positionAndCamera;

    public Animator anim;

    Camera playerCamera;

    Canvas playerUI;

    public int playerviewId;

    private void Awake()
    {
        if (!photonView.IsMine) return;

        anim = GetComponent<Animator>();

        playerviewId = photonView.ViewID;

    }

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        if (!photonView.IsMine) return;

        // gameManager의 awake가 실행되고 실행되어야 하기 떄문에 photonManager에서 사용 불가
        PlayerAdd_Call();

        positionAndCamera.transform.GetChild(0).gameObject.SetActive(true);

        positionAndCamera.transform.GetChild(1).gameObject.SetActive(true);

        playerCamera = positionAndCamera.GetComponentInChildren<Camera>();

        playerUI = positionAndCamera.GetComponentInChildren<Canvas>();

        ngBattleUIManager.instance.Initialize(playerUI, this);
    }

    public void PlayerAdd_Call()
    {
        photonView.RPC("PlayerAdd_RPC", RpcTarget.All);
    }

    [PunRPC]
    public void PlayerAdd_RPC()
    {
        if (GameManager.instance == null)
        {
            Debug.Log("GameManager is null");
        }
        else
        {
            GameManager.instance.PlayerAdd();
        }
    }



    public void Cast(ngMonsterSkill castingSKill, ngMonsterController target)
    {
        if (!photonView.IsMine) return;
        
        if (monsterInfo.skillList.Contains(castingSKill))
        {
            photonView.RPC("Cast_RPC", RpcTarget.All, monsterInfo.skillList.IndexOf(castingSKill), target.photonView.ViewID);
        }
        else
        {
            return;
        }
    }

    [PunRPC]
    public void Cast_RPC(int castingSkill, int targetViewID)
    {
        // targetViewID: viewID로 찾기

        // castingSkill: 좌상부터 시계방향으로 0~3
        // 스킬 리스트안에 들어있는 순서대로 0~3

        GameManager.instance.CastOrder(this, monsterInfo.skillList[castingSkill], targetViewID);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
