using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ngPhotonManager : MonoBehaviourPunCallbacks
{
    public static ngPhotonManager instance;

    private ClientState photonState = 0;

    public List<GameObject> playerPosition = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }

        PhotonNetwork.AutomaticallySyncScene = true;

        Screen.SetResolution(1960, 1080, true);

        PhotonNetwork.ConnectUsingSettings();
    }

    private void Start()
    {
        //PhotonNetwork.ConnectUsingSettings();

        // 닉네임 결정은 ui 및 db 설정 후
        //PhotonNetwork.NickName = userId;
    }

    // 마스터 서버에 접속했을 때(로비 메인화면)
    public override void OnConnected()
    {
        base.OnConnected();
    }

    public void randMatchingStart()
    {
        //PhotonNetwork.JoinRandomRoom();

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        PhotonNetwork.JoinRandomOrCreateRoom
            (
            roomOptions: options
            );
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
            //print($"{photonState} 에서 {PhotonNetwork.NetworkClientState}으로 변경됨");

            ngUIManager.instance.loadingUI_LoadingText.text = ($"{photonState} changed into {PhotonNetwork.NetworkClientState}");

            if (ngUIManager.instance.loadingUI_LoadingSlider.value <= GetProgress(PhotonNetwork.NetworkClientState))
            {
                ngUIManager.instance.loadingUI_LoadingSlider.value = GetProgress(PhotonNetwork.NetworkClientState);
            }
            
            photonState = PhotonNetwork.NetworkClientState;
        }
    }
    
    private float GetProgress(Photon.Realtime.ClientState state)
    {
        // 로딩 퍼센테이지 

        switch (state)
        {
            case Photon.Realtime.ClientState.Disconnected:
                return 0f; 
            case Photon.Realtime.ClientState.ConnectingToNameServer:
                return 0.1f; 
            case Photon.Realtime.ClientState.ConnectedToNameServer:
                return 0.3f;
            case Photon.Realtime.ClientState.Authenticating:
                return 0.4f;
            case Photon.Realtime.ClientState.ConnectingToMasterServer:
                return 0.5f; 
            case Photon.Realtime.ClientState.ConnectedToMasterServer:
                return 0.7f; 
            case Photon.Realtime.ClientState.JoiningLobby:
                return 0.9f; 
            case Photon.Realtime.ClientState.JoinedLobby:
                return 1.0f; 
            default:
                return 0f;
        }
    }



    public override void OnConnectedToMaster()
    {
        //서버 접속

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        //base.OnJoinedLobby();
        ngUIManager.instance.UIChangeInto(ngUIManager.instance.LobbyUI);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("masterClient");

            if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                PhotonNetwork.LoadLevel("ngGame");
            }
        }
        else
        {
            Debug.Log("otherClient");
        }
    }

    public override void OnJoinedRoom()
    {
        
    }

   

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
    }

    public void LeftRoom()
    {
        PhotonNetwork.LeaveRoom();
    }


    public void OnEnableGameManager()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OndisableGameManager()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "ngGame")
        {
            if (PhotonNetwork.IsMasterClient)
            {
                GameObject player = PhotonNetwork.Instantiate("Slime", playerPosition[1].transform.position, playerPosition[1].transform.rotation);

                player.GetComponent<ngMonsterController>().positionAndCamera = playerPosition[1];
            }
            else
            {
                GameObject player = PhotonNetwork.Instantiate("Slime", playerPosition[0].transform.position, playerPosition[0].transform.rotation);

                player.GetComponent<ngMonsterController>().positionAndCamera = playerPosition[0];
            }
        }
    }


    
}