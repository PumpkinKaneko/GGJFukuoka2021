using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace GanGanKamen.Lobby
{
    public class LobbyUI : MonoBehaviour
    {
        public bool CanCtrl { get { return canCtrl; } }
        public string UserName { get { return name_InputField.text; } }
        [SerializeField] LobbyPhotonManager photonManager;
        [SerializeField] private InputField room_InputField;
        [SerializeField] private InputField name_InputField;
        [SerializeField] private Button create_Button;
        [SerializeField] private Button list_Button;
        [SerializeField] private Button close_Button;
        [SerializeField] private GameObject listWindow;
        [SerializeField] private RectTransform content;
        [SerializeField] private GameObject roomItemPrefab;
        [SerializeField] private GameObject barrier;

        private const int maxPlayers = 4;
        private List<RoomItem> roomItems;
        private bool canCtrl = false;
        private bool preCanCtrl = false;
        // Start is called before the first frame update

        void Start()
        {
            Init();
        }

        // Update is called once per frame
        void Update()
        {
            CanCtrlUpdate();
        }

        public void ListOpen()
        {
            if (listWindow.activeSelf == false)
                listWindow.SetActive(true);
            if (name_InputField.gameObject.activeSelf == true)
                name_InputField.gameObject.SetActive(false);
            if (list_Button.gameObject.activeSelf == true)
                list_Button.gameObject.SetActive(false);
        }

        public void ListClose()
        {
            if (listWindow.activeSelf)
                listWindow.SetActive(false);
            if (name_InputField.gameObject.activeSelf == false)
                name_InputField.gameObject.SetActive(true);
            if (list_Button.gameObject.activeSelf == false)
                list_Button.gameObject.SetActive(true);
        }

        public void CreateRoom()
        {
            
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;
            roomOptions.MaxPlayers = (byte)maxPlayers;
            if (string.IsNullOrEmpty(name_InputField.text))
            {
                AotoUserName();
            }
            if (string.IsNullOrEmpty(room_InputField.text))
            {
                room_InputField.text = "Room" + PhotonNetwork.CountOfRooms.ToString("D3");
            }
            PhotonNetwork.CreateRoom(room_InputField.text, roomOptions, TypedLobby.Default);
            CtrlDown();
        }

        public void RoomListReset()
        {
            foreach(var obj in roomItems)
            {
                Destroy(obj.gameObject);
            }
            roomItems.Clear();
        }

        public void CreateRoomItem(string roomName, int nowPlayerNumber,int maxPlayerNumber)
        {
            GameObject roomObj = Instantiate(roomItemPrefab, content);
            var item = roomObj.GetComponent<RoomItem>();
            item.Init(roomName, nowPlayerNumber,maxPlayerNumber);
        }

        public void CtrlDown()
        {
            if (canCtrl == false) return;
            canCtrl = false;
        }

        public void CtrlOn()
        {
            if (canCtrl == true) return;
            canCtrl = true;
        }

        public void AotoUserName()
        {
            name_InputField.text = "Player" + PhotonNetwork.CountOfPlayers.ToString("D3");
        }

        private void Init()
        {
            roomItems = new List<RoomItem>();
            list_Button.onClick.AddListener(() => ListOpen());
            close_Button.onClick.AddListener(() => ListClose());
            create_Button.onClick.AddListener(() => CreateRoom());
            list_Button.interactable = false;
            create_Button.interactable = false;
            close_Button.interactable = false;
            photonManager.Connect();
        }

        private void CanCtrlUpdate()
        {
            if(canCtrl != preCanCtrl)
            {
                switch (canCtrl)
                {
                    case true:
                        if (barrier.activeSelf) barrier.SetActive(false);
                        list_Button.interactable = true;
                        create_Button.interactable = true;
                        close_Button.interactable = true;
                        break;
                    case false:
                        ListClose();
                        list_Button.interactable = false;
                        create_Button.interactable = false;
                        close_Button.interactable = false;
                        break;
                }

                preCanCtrl = canCtrl;
            }
        }
    }

}

