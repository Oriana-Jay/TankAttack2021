using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    private readonly string gameVersion = "v1.0";
    private string UserId = "Zackiller";

    void Awake()
    {
        // 게임 버전 지정
        PhotonNetwork.GameVersion = gameVersion;
        // 유저명 지정
        PhotonNetwork.NickName = UserId;

        // 서버접속
        PhotonNetwork.ConnectUsingSettings();        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photoon Server!!!");
        PhotonNetwork.JoinRandomRoom(); // 랜덤한 룸에 접속 시도
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"code={returnCode}, msg={message}");
    }

}
