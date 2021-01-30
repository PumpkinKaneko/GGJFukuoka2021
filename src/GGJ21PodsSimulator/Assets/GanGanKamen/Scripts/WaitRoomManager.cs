using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace GanGanKamen.Wait
{
    public class WaitRoomManager : MonoBehaviourPunCallbacks
    {
        private RoomData roomData;

        public void Init(string playerName)
        {
            GameObject obj = PhotonNetwork.Instantiate("Prefabs/WaitTest", Vector3.zero, Quaternion.identity, 0);
            var waitScript = obj.GetComponent<TestWait>();
            waitScript.enabled = true;
            waitScript.Init(playerName);
            /*
            if (PhotonNetwork.IsMasterClient)
            {
                var materialNums = new int[1] {0};
                var dataObj = new GameObject("RoomData");
                dataObj.AddComponent<RoomData>();
                roomData = dataObj.GetComponent<RoomData>();
                roomData.Init(materialNums);
               
            }
            */
        }
        /*
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            if (newPlayer.IsMasterClient == false)
            {
                Debug.Log(newPlayer.NickName);
                var materialNums = new int[4] { 0, 1, 2, 3 };
                var materialNumList = new List<int>();
                materialNumList.AddRange(materialNums);
                foreach (int i in roomData.NowMaterialsList)
                {
                    materialNumList.Remove(i);
                }
                var matNum = materialNumList[0];
                GameObject obj = PhotonNetwork.Instantiate("Prefabs/WaitTest", Vector3.zero, Quaternion.identity, 0);
                obj.GetComponent<PhotonView>().TransferOwnership(newPlayer);
                var waitScript = obj.GetComponent<TestWait>();
                waitScript.enabled = true;
                waitScript.Init(newPlayer.NickName);
                roomData.PlayerJoined(matNum);
            }
                         
        }
        */
        
    }
}

