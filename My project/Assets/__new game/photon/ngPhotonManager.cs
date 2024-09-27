using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ngPhotonManager : MonoBehaviourPunCallbacks
{
    private ClientState photonState = 0;

    List<int> playerArr = new();

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        Screen.SetResolution(1960, 1080, true);

        PhotonNetwork.ConnectUsingSettings();
    }

    private void Start()
    {
        //PhotonNetwork.ConnectUsingSettings();

        // �г��� ������ ui �� db ���� ��
        //PhotonNetwork.NickName = userId;
    }

    // ������ ������ �������� ��(�κ� ����ȭ��)
    public override void OnConnected()
    {
        base.OnConnected();
    }

    public void randMatchingStart()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions ro = new RoomOptions { IsOpen = true, MaxPlayers = 2 };

        PhotonNetwork.CreateRoom(null,ro);
    }


    


    private void Update()
    {
        if (PhotonNetwork.NetworkClientState != photonState)
        {
            //print($"{photonState} ���� {PhotonNetwork.NetworkClientState}���� �����");
            photonState = PhotonNetwork.NetworkClientState;
        }
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
    }

    

    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom("Room1", options, null);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

    }

    public override void OnJoinedRoom()
    {
        GameObject player = PhotonNetwork.Instantiate("player", new Vector3(0, 0, 0), Quaternion.identity);
    }
}
