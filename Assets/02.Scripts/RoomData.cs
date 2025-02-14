using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomData : MonoBehaviour
{
    private TMP_Text roomInfoText;
    private RoomInfo _roomInfo;
    
    public RoomInfo RoomInfo
    {
        get
        {
            return  _roomInfo;    
        }

        set
        {
            _roomInfo = value;
            roomInfoText.text = $"{_roomInfo.Name} ({_roomInfo.PlayerCount} / {_roomInfo.MaxPlayers})";
            
            //버튼 클릭 이벤트에 함수 연결
            GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnEnterRoom(_roomInfo.Name));
            //GetComponent<UnityEngine.UI.Button>().onClick.AddListner(delegate() {OnEnterRoom(_roomInfo.Name)}); 
            //이 delegate를 간략하게 사용하기 위해서 위의 람다식 로직을 이용. 

        }


    }

    // Start is called before the first frame update
    void Awake()
    {
        roomInfoText = GetComponentInChildren<TMP_Text>();
    }

    void OnEnterRoom(string roomName)
    {   
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 30;

        PhotonNetwork.JoinOrCreateRoom(roomName, ro , TypedLobby.Default);
    }

   
}
