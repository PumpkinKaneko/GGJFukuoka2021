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
            PhotonNetwork.JoinLobby();
            lobbyUI.CtrlOn();
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
            lobbyUI.CtrlOn();
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
            lobbyUI.CtrlOn();
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
            if (PhotonNetwork.IsConnected)
            {
                StartCoroutine(LoadSceneCoroutine());
                return;
            }

            PhotonNetwork.ConnectUsingSettings();
        }

        private IEnumerator LoadSceneCoroutine()
        {
            lobbyUI.CtrlDown();
            DontDestroyOnLoad(gameObject);
            var userName = lobbyUI.UserName;
            PhotonNetwork.LeaveLobby();
            PhotonNetwork.NickName = lobbyUI.UserName;
            PhotonNetwork.IsMessageQueueRunning = false;
            yield return GameManage.Instance.LoadSceneAsync(new RoomScene(GameManage.Instance, "WaitRoom"), LoadSceneMode.Single);
            PhotonNetwork.IsMessageQueueRunning = true;            
            var waitMng = GameObject.Find("WaitRoomManager")
                .GetComponent<GanGanKamen.Wait.WaitRoomManager>();
            waitMng.Init(userName);            
            Destroy(gameObject);
        }
    }
}

