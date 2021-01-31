using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class HumanSampletScene : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private Transform human_start_transform;
    [SerializeField]
    private GameObject m_play_sound_panel;
    [SerializeField]
    private Transform[] earPhone_start_transforms; 

    public void Init(string playerName,int materialNum, int characterNumber)
    {
        if(characterNumber == 0)
        {
            PhotonNetwork.Instantiate("Prefabs/human_player",
                human_start_transform.position, human_start_transform.rotation);
        }
        else
        {
            PhotonNetwork.Instantiate("Prefabs/AirPods_player", earPhone_start_transforms[characterNumber].position, 
                earPhone_start_transforms[characterNumber].rotation);
        }

        GameManage.Instance.IsMatched = true;
    }

    /*
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
        PhotonNetwork.Instantiate("Prefabs/" + PlayerPrefabName, m_start_transform.position, m_start_transform.rotation);

        GameManage.Instance.IsMatched = true;

        base.OnJoinedRoom();
    }
    */
}
