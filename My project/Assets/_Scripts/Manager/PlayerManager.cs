using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public List<GameObject> A_Team_Players = new();
    public List<GameObject> B_Team_Players = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // rpc�� �Ķ���ͷ� gameobject�� ���� �� ���� ������ PhotonView ID�� ���� �޾ƾ� ��
    // �׸��� allbuffered�ΰ� �װɷ� �����ϰ� 
    [PunRPC]
    void AddPlayerList_RPC(GameObject _player, string team)
    {
        switch (team)
        {
            case "A":
                A_Team_Players.Add(_player);
                _player.gameObject.layer = 10;
                break;

            case "B":
                B_Team_Players.Add(_player);
                _player.gameObject.layer = 11;
                break;
        }
    }
}