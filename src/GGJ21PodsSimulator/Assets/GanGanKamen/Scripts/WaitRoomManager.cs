using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace GanGanKamen.Wait
{
    public class WaitRoomManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private WaitRoomUI roomUI;
        [SerializeField] private Transform spawnPosition;
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

    }
}

