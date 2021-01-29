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
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
       // GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }

    public override void OnConnectedToMaster()
    {
        // "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
        //PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        //キャラクターを生成
        GameObject monster = PhotonNetwork.Instantiate("monster", Vector3.zero, Quaternion.identity, 0);
        //自分だけが操作できるようにスクリプトを有効にする
        MonsterScript monsterScript = monster.GetComponent<MonsterScript>();
        monsterScript.enabled = true;
    }
}

