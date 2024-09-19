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


    // rpc는 파라미터로 gameobject를 받을 수 없기 때문에 PhotonView ID를 통해 받아야 함
    // 그리고 allbuffered인가 그걸로 저장하게 
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