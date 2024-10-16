using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonTest : MonoBehaviourPunCallbacks
{
    private ClientState photonState = 0;

    List<int> playerArr = new();

    private void Start()
    {
        Screen.SetResolution(1960, 1080, true);
        PhotonNetwork.ConnectUsingSettings();
        
    }

    private void Update()
    {
        if (PhotonNetwork.NetworkClientState != photonState)
        {
            //print($"{photonState} 에서 {PhotonNetwork.NetworkClientState}으로 변경됨");
            photonState = PhotonNetwork.NetworkClientState;
        }
    }


    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 6;
        PhotonNetwork.JoinOrCreateRoom("Room1", options, null);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

    }

    public override void OnJoinedRoom()
    {
        // switch나 if문을 통해 samurai 바꿔 받기
        string a = "Paladine";
        //string b = "Samurai";

        GameObject player = PhotonNetwork.Instantiate(a, new Vector3(0, 0, 0), Quaternion.identity);
        //BuffManager.instance.AddPlayerList(player.GetComponent<PlayerStatus>());

        //PlayerManager.instance.AddPlayerList(player, "A");
    }
}
