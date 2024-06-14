using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;

    [SerializeField] GameObject playerPrefabTeamRed;
    [SerializeField] GameObject playerPrefabTeamBlue;
    [HideInInspector] public Transform cameraPlayer;

    [SerializeField] GameObject gameScreen;


    private void Awake()
    {
        instance = this;
        gameScreen.SetActive(false);
    }

    public void StartGame()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            photonView.RPC(nameof(CreateAvatar), RpcTarget.AllBuffered);
        }


        //photonView.RPC(nameof(CreatePlayerAvatar), PhotonNetwork.LocalPlayer, NetworkManager.instance.inputNickName.text);

    }

    [PunRPC]
    void CreateAvatar()
    {
        PhotonNetwork.LocalPlayer.NickName = NetworkManager.instance.inputNickName.text;

        if (PhotonNetwork.PlayerList[0] == PhotonNetwork.LocalPlayer)
        {
            Vector3 _pos = new Vector2(-1f, -1f);
            GameObject player = PhotonNetwork.Instantiate(playerPrefabTeamRed.name, _pos, Quaternion.identity);
        }
        else
        {
            Vector3 _pos = new Vector2(1f, -1f);
            GameObject player = PhotonNetwork.Instantiate(playerPrefabTeamBlue.name, _pos, Quaternion.identity);
        }

        gameScreen.SetActive(true);
        NetworkManager.instance.loadingScreen.SetActive(false);

    }


    [PunRPC]
    //pode chamar o metodo de forma remota, chamar no computador alheio ou s� de voce ou de uma pessoa especifica. Parecido com serializedfield, se p�e na frente
    void CreatePlayerAvatar(string _nickName)
    {
        PhotonNetwork.LocalPlayer.NickName = _nickName;


        //Vector3 _pos = new Vector3(Random.Range(-3f, 3f), 2f, Random.Range(-3f, 3f));
        //posi��es aleatorias

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            //Instantiate
            //instantiate normal � local
            Vector3 _pos = new Vector2(-1f, -1f);
            PhotonNetwork.Instantiate(playerPrefabTeamRed.name, _pos, Quaternion.identity);
            //quaternion controla a rota��o, 4 valores
        }
        else
        {
            Vector3 _pos = new Vector2(1f, -1f);
            PhotonNetwork.Instantiate(playerPrefabTeamBlue.name, _pos, Quaternion.identity);
        }

        gameScreen.SetActive(true);
        NetworkManager.instance.loadingScreen.SetActive(false);

    }

}
