using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace GanGanKamen.Wait
{
    public class WaitRoomUI : MonoBehaviour
    {
        [SerializeField] private Text roomNameText;
        [SerializeField] private Text roomMembersText;
        [SerializeField] private Button startButton;

        public void TextSet()
        {
            roomNameText.text = PhotonNetwork.CurrentRoom.Name;
            var nowNum = PhotonNetwork.CurrentRoom.PlayerCount;
            var txt = nowNum.ToString() + "/" +
                ((int)PhotonNetwork.CurrentRoom.MaxPlayers).ToString();
            if (nowNum <= 1)
            {
                roomMembersText.color = Color.red;
            }
            else
            {
                roomMembersText.color = Color.white;
            }
            roomMembersText.text = txt;
        }

        public void StartButtonOn()
        {
            if (startButton.gameObject.activeSelf == false)
                startButton.gameObject.SetActive(true);
        }
    }
}



