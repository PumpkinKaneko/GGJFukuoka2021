using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class HumanSampletScene : MonoBehaviourPunCallbacks
{
    public string RoomName = "ConnectedRoom";
    public string PlayerPrefabName;

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
        PhotonNetwork.Instantiate("Prefabs/" + PlayerPrefabName, Vector3.zero, Quaternion.identity);

        base.OnJoinedRoom();
    }
}
