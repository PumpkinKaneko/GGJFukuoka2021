using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SamplePun : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.ConnectUsingSettings();
        GameObject monster = PhotonNetwork.Instantiate("monster", Vector3.zero, Quaternion.identity, 0);
        MonsterScript monsterScript = monster.GetComponent<MonsterScript>();
        monsterScript.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
       // GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        // "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
        //PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        GameObject monster = PhotonNetwork.Instantiate("monster", Vector3.zero, Quaternion.identity, 0);
    }
}

