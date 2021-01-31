using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

namespace GanGanKamen.Wait
{
    public class WaitRoomManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private WaitRoomUI roomUI;
        [SerializeField] private Transform spawnPosition;

        private TestWait _waitPlayer;

        public void Init(string playerName)
        {
            roomUI.TextSet();
            var spawnhight = spawnPosition.position.y + PhotonNetwork.CountOfPlayersInRooms * 0.25f;
            GameObject obj = PhotonNetwork.Instantiate("Prefabs/WaitTest", 
                new Vector3(spawnPosition.position.x,spawnhight,spawnPosition.position.z), Quaternion.identity, 0);
            var waitScript = obj.GetComponent<TestWait>();
            waitScript.enabled = true;
            waitScript.Init(playerName);
            if (PhotonNetwork.IsMasterClient)
            {
                roomUI.StartButtonOn();
            }
            roomUI.SetWaitPlayer(waitScript);
            _waitPlayer = waitScript;
            roomUI.Init();
            
        }

        public void GameStart()
        {
            Shuffle();
            _waitPlayer.GameStart();
        }
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            roomUI.TextSet();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            roomUI.TextSet();
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            base.OnMasterClientSwitched(newMasterClient);
            if(newMasterClient == PhotonNetwork.LocalPlayer)
            {
                roomUI.StartButtonOn();
            }
        }

        private void Update()
        {
            roomUI.ColorUpdate();
        }

        private void Shuffle()
        {
            var playerList = new List<GameObject>();
            foreach(var obj in GameObject.FindGameObjectsWithTag("Player"))
            {
                playerList.Add(obj);
            }
            Debug.Log(GameObject.FindGameObjectsWithTag("Player").Length);
            playerList = playerList.OrderBy(a => System.Guid.NewGuid()).ToList();
            for(int i = 0; i < playerList.Count; i++)
            {
                var id = playerList[i].GetComponent<PhotonView>().ViewID;
                playerList[i].GetComponent<TestWait>().SetCharacter(i,id);
            }
        }
    }
}

