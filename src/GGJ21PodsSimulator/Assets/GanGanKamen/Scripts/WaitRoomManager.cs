using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace GanGanKamen.Wait
{
    public class WaitRoomManager : MonoBehaviour
    {
        [SerializeField] private Material[] materials;
        private string _playerName;

        public void Init(string playerName)
        {
            _playerName = playerName;
            var num = PhotonNetwork.CountOfPlayersInRooms;
            Debug.Log("materialNum" + num.ToString());
            Debug.Log(materials.Length);
            GameObject obj  = PhotonNetwork.Instantiate("Prefabs/WaitTest", Vector3.zero, Quaternion.identity, 0);
            //DontDestroyOnLoad(obj);
            var waitScript = obj.GetComponent<GanGanKamen.Test.TestWait>();
            waitScript.enabled = true;
            waitScript.Init(playerName,materials[num]);

        }
    }
}

