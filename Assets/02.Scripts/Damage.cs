using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Damage : MonoBehaviourPunCallbacks
{
    private List<MeshRenderer> renderers = new List<MeshRenderer>();
    public int hp = 100;
    private PhotonView pv;
    int killCount = 0;
    public TMPro.TMP_Text killList;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        GetComponentsInChildren<MeshRenderer>(renderers);

        killList = GameObject.FindGameObjectWithTag("KILL_COUNT").GetComponent<TMPro.TMP_Text>();
        InitProperties();
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("CANNON"))
        {
            //string shooter = coll.gameObject.GetComponent<Cannon>().shooter;
            int actorNumber = coll.gameObject.GetComponent<Cannon>().actorNumber;
            Player shooter = PhotonNetwork.CurrentRoom.GetPlayer(actorNumber);

            hp -= 10;
            if (hp <= 0)
            {
                StartCoroutine(TankDestroy(shooter.NickName));
                //StartCoruotine("TankDestroy", shooter);
                IncreaseKillCount(shooter);
            }
        }
    }

    void InitProperties()
    {
        Hashtable ht = new Hashtable();
        ht.Add("KILL_COUNT", 0);

        PhotonNetwork.LocalPlayer.SetCustomProperties(ht);
    }
    
    void IncreaseKillCount(Player shooter)
    {
        

        killCount = (int)shooter.CustomProperties["KILL_COUNT"];
        Debug.Log($"Player = {shooter.NickName} Kill Count = {killCount}");
        Hashtable ht = new Hashtable();
        ht.Add("KILL_COUNT", ++killCount);

        shooter.SetCustomProperties(ht);
        Debug.Log($"Player = {shooter.NickName} Kill Count = {killCount}");
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        Debug.Log($"{changedProps["KILL_COUNT"]}");
        Player[] players = PhotonNetwork.PlayerList;
        string msg = "";

        for(int i=0; i<players.Length; i++)
        {
            int _killCount = (int)players[i].CustomProperties["KILL_COUNT"];
            msg += $"{players[i].NickName} Kill Count={_killCount}\n";
        }
        killList.text = "";
        killList.text = msg;
    }

    IEnumerator TankDestroy(string shooter)
    {
        string msg = $"\n<color=#00ff00>{pv.Owner.NickName}</color> is killed by <color=#ff0000>{shooter}</color>";
        GameManager.instance.messageText.text += msg; 

        // 발사로직을 정지
        // 랜더러 컴포넌틀 비활성화
        GetComponent<BoxCollider>().enabled = false;
        if (pv.IsMine)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        foreach(var mesh in renderers) mesh.enabled = false;

        // 5초 waitting
        yield return new WaitForSeconds(5.0f);

        // 불규칙한 위치로 이동
        Vector3 pos = new Vector3(Random.Range(-150.0f, 150.0f),
                                  5.0f,
                                  Random.Range(-150.0f, 150.0f));        

        transform.position = pos;

        // 랜더러 컴포넌틀 활성화
        hp = 100;
        GetComponent<BoxCollider>().enabled = true;
        if (pv.IsMine)
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
        foreach(var mesh in renderers) mesh.enabled = true;
    }
}
