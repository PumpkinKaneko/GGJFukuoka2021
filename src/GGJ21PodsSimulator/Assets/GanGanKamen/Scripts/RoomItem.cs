using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace GanGanKamen.Lobby
{
    public class RoomItem : MonoBehaviour
    {
        [SerializeField] private Text nameText;
        [SerializeField] private Text memberText;
        [SerializeField] private Button joinButton;

        private string _roomName;
        private int _playerNumber;

        public void Init(string roomName, int playerNumber, int maxPlayerNumber)
        {
            _roomName = roomName;
            _playerNumber = playerNumber;
            nameText.text = roomName;
            memberText.text = _playerNumber.ToString() + "/" + maxPlayerNumber.ToString();
            joinButton.onClick.AddListener(() => JoinRoom(_roomName));
        }

        public void JoinRoom(string roomName)
        {
            PhotonNetwork.JoinRoom(roomName);
        }
    }
}


