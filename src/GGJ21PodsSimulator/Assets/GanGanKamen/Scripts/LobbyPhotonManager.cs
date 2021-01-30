using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace GanGanKamen.Lobby
{
    public class LobbyPhotonManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private LobbyUI lobbyUI;

        public override void OnConnectedToMaster()
        {
            Debug.Log("joinLobby");
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            Debug.Log("joined");
            StartCoroutine(LoadSceneCoroutine());
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);
            /*
            Debug.Log("faild");
            room_InputField.textComponent.color = Color.red;
            room_InputField.text = "<color=#ff0000>指定の部屋は既に存在しています</color>";
            */
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            
            lobbyUI.RoomListReset();
            base.OnRoomListUpdate(roomList);
            Debug.Log("RoomlistUpdate");
            foreach (var info in roomList)
            {
                var roomName = info.Name;
                var nowMember = info.PlayerCount;
                var maxMember = (int)info.MaxPlayers;
                lobbyUI.CreateRoomItem(roomName, nowMember, maxMember);
            }
            
        }

        public void Connect()
        {
            DontDestroyOnLoad(gameObject);
            if (PhotonNetwork.IsConnected) return;
            PhotonNetwork.ConnectUsingSettings();
        }

        private IEnumerator LoadSceneCoroutine()
        {
            lobbyUI.CtrlDown();
            PhotonNetwork.LeaveLobby();
            PhotonNetwork.IsMessageQueueRunning = false;
            yield return SceneManager.LoadSceneAsync("WaitRoom", LoadSceneMode.Single);
            PhotonNetwork.IsMessageQueueRunning = true;
            Debug.Log(PhotonNetwork.IsMessageQueueRunning);
            var waitMng = GameObject.Find("WaitRoomManager")
                .GetComponent<GanGanKamen.Wait.WaitRoomManager>();
            waitMng.Init(lobbyUI.UserName);
            Destroy(gameObject);
        }
    }
}

