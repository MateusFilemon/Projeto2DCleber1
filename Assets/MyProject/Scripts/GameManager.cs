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
        photonView.RPC(nameof(CreatePlayerAvatar), PhotonNetwork.LocalPlayer, NetworkManager.instance.inputNickName.text);

    }

    [PunRPC]
    //pode chamar o metodo de forma remota, chamar no computador alheio ou só de voce ou de uma pessoa especifica. Parecido com serializedfield, se põe na frente
    void CreatePlayerAvatar(string _nickName)
    {
        PhotonNetwork.LocalPlayer.NickName = _nickName;


        //Vector3 _pos = new Vector3(Random.Range(-3f, 3f), 2f, Random.Range(-3f, 3f));
        //posições aleatorias

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            //Instantiate
            //instantiate normal é local
            Vector3 _pos = new Vector2(-1f, -1f);
            PhotonNetwork.Instantiate(playerPrefabTeamRed.name, _pos, Quaternion.identity);
            //quaternion controla a rotação, 4 valores
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
