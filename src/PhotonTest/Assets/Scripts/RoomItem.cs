using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomItem : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text memberText;
    [SerializeField] private Button joinButton;

    private string _roomName;
    private int _playerNumber;
    private int _maxNumber;

    public void Init(string roomName,int playerNumber,int maxNumber)
    {
        _roomName = roomName;
        _playerNumber = playerNumber;
        _maxNumber = maxNumber;
        nameText.text = roomName;
        memberText.text = _playerNumber.ToString() + "/" + _maxNumber.ToString();
        joinButton.onClick.AddListener(() => JoinRoom(_roomName));
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
}
