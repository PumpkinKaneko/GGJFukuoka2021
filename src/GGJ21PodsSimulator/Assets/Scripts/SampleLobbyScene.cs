using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleLobbyScene : MonoBehaviourPunCallbacks
{
    public string RoomName = "ConnectedRoom";

    private void Start()
    {
        // マスターサーバーに接続
        PhotonNetwork.ConnectUsingSettings();
        
    }

    
    /// <summary>
    /// 接続完了のコールバック
    /// </summary>
    public override void OnConnectedToMaster()
    {
        // 指定した名称のルームに接続
        PhotonNetwork.JoinOrCreateRoom(this.RoomName, new RoomOptions(), TypedLobby.Default);

        base.OnConnectedToMaster();
    }


    /// <summary>
    /// マッチング成功時のコールバック
    /// </summary>
    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("Prefabs/SamplePlayer", Vector3.zero, Quaternion.identity);

        base.OnJoinedRoom();
    }
}
