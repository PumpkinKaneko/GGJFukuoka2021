using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MachingManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Text text;
    public const string TYPE_KEY = "Type";
    private string type = "A";
    private void Awake()
    {
        text.text = "待機中...";
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected) return;
        PhotonNetwork.ConnectUsingSettings();
        text.text = "マッチング中...";
        
    }

    public override void OnConnectedToMaster()
    {
        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = 
            new ExitGames.Client.Photon.Hashtable { { TYPE_KEY, type } };
        var maxPlayer = (byte)2;
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, maxPlayer);
    }

    public override void OnJoinedRoom()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            text.text = "マッチングしました";
            PhotonNetwork.IsMessageQueueRunning = false;
            SceneManager.LoadScene("Main");
        }

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            text.text = "マッチングしました";
            PhotonNetwork.IsMessageQueueRunning = false;
            SceneManager.LoadScene("Main");
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    private void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties =
           new ExitGames.Client.Photon.Hashtable { { TYPE_KEY, type } };
        roomOptions.CustomRoomProperties = expectedCustomRoomProperties;
        PhotonNetwork.CreateRoom(null, roomOptions, null);
        text.text = "部屋を作成します...";
        Debug.Log("CreateRoom");
    }
}
