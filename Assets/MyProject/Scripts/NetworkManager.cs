using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using TMPro;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance; //singleton, um unico objeto com essa classe(NetworkManager)

    public TMP_InputField inputNickName;
    public GameObject menuScreen;
    public GameObject loadingScreen;
    [SerializeField] int playerCount = 2;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            menuScreen.SetActive(true);
            loadingScreen.SetActive(false);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        
        Debug.Log("Start");
    }

    public void ConnectToPhoton()
    {
        PhotonNetwork.ConnectUsingSettings();
        loadingScreen.SetActive(true);
        menuScreen.SetActive(false);
        Debug.Log("ConnectToPhoton");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = (byte)playerCount;

        PhotonNetwork.CreateRoom(null, options);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        Debug.Log("Playercount: " + PhotonNetwork.CurrentRoom.PlayerCount);
        Debug.Log("Nickname: " + inputNickName.text);
       
        GameManager.instance.StartGame();

        //photonView.RPC("CreatePlayerAvatar", PhotonNetwork.LocalPlayer);
        //photonView, chamado remoto
    }





}

