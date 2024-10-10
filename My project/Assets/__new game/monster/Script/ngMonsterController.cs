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

        // gameManager�� awake�� ����ǰ� ����Ǿ�� �ϱ� ������ photonManager���� ��� �Ұ�
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
        // targetViewID: viewID�� ã��

        // castingSkill: �»���� �ð�������� 0~3
        // ��ų ����Ʈ�ȿ� ����ִ� ������� 0~3

        GameManager.instance.CastOrder(this, monsterInfo.skillList[castingSkill], targetViewID);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
