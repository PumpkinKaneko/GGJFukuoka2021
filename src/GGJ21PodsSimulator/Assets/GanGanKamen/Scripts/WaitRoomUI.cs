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
        public int ColorNumber { get { return colorNumber; } }
        [SerializeField] private Text roomNameText;
        [SerializeField] private Text roomMembersText;
        [SerializeField] private Button startButton;
        [SerializeField] private Button colorButton;
        [SerializeField] private Color[] colors;
        [SerializeField] private WaitRoomManager roomManager;
        private TestWait _waitPlayer;
        private int colorNumber = 0;
        private int preColor = 0;

        public void Init()
        {
            startButton.onClick.AddListener(() => GameStart());
            colorButton.onClick.AddListener(() => ColorChange());
        }

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

        public void SetWaitPlayer(TestWait waitPlayer)
        {
            _waitPlayer = waitPlayer;
        }

        public void ColorChange()
        {
            if (colorNumber >= 3) colorNumber = 0;
            else colorNumber += 1;
        }

        public void GameStart()
        {
            startButton.interactable = false;
            startButton.gameObject.SetActive(false);
            colorButton.interactable = false;
            roomManager.GameStart();
        }

        public void ColorUpdate()
        {
            if(preColor != colorNumber)
            {
                _waitPlayer.SetMaterial(colorNumber);
                colorButton.image.color = colors[colorNumber]; 
                preColor = colorNumber;
            }
        }
    }
}



