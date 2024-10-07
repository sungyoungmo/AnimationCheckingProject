using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ngMonsterController : MonoBehaviourPun, IPunObservable
{
    public ngMonster monsterInfo;

    public GameObject positionAndCamera;

    public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        PlayerAdd_Call();
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
            Debug.Log(1);
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
