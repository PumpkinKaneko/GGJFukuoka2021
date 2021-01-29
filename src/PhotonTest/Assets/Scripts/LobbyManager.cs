using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField room_InputField;
    [SerializeField] private Slider people_Slider;
    [SerializeField] private Text people_Num;
    [SerializeField] private Button create_Button;
    [SerializeField] private Button list_Button;
    [SerializeField] private Button close_Button;
    [SerializeField] private GameObject listWindow;
    [SerializeField] private GameObject RoomPrefab;
    [SerializeField] private Text debug;
    [SerializeField] private RectTransform content;
    [SerializeField] private GameObject roomItemPrefab;
    private string roomName = "";
    private int playerNum = 0;
    private List<RoomInfo> roomDispList;
    private List<RoomItem> roomItems;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        ParamaterUpdate();
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        roomOptions.MaxPlayers = (byte)playerNum;
        if (string.IsNullOrEmpty(room_InputField.text))
        {
            room_InputField.text = "MyRoom";
        }
        PhotonNetwork.CreateRoom(room_InputField.text, roomOptions, TypedLobby.Default);
    }

    public void ListOpen()
    {
        if (listWindow.activeSelf == false)
            listWindow.SetActive(true);

    }

    public void ListClose()
    {
        if (listWindow.activeSelf)
            listWindow.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("joinLobby");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("joined");
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.IsMessageQueueRunning = false;
        SceneManager.LoadScene("Main");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        RoomListReset();
        base.OnRoomListUpdate(roomList);
        Debug.Log("RoomlistUpdate");
        roomDispList = roomList;
        foreach (var info in roomList)
        {
            var roomName = info.Name;
            var nowMember = info.PlayerCount;
            var maxMember = (int)info.MaxPlayers;
            GameObject roomObj = Instantiate(roomItemPrefab, content);
            var item = roomObj.GetComponent<RoomItem>();
            item.Init(roomName,nowMember,maxMember);
        }

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("faild");
        room_InputField.textComponent.color = Color.red;
        room_InputField.text = "<color=#ff0000>指定の部屋は既に存在しています</color>";
    }


    private void Init()
    {
        roomDispList = new List<RoomInfo>();
        roomItems = new List<RoomItem>();
        list_Button.onClick.AddListener(() => ListOpen());
        close_Button.onClick.AddListener(() => ListClose());
        create_Button.onClick.AddListener(() => CreateRoom());
        if (PhotonNetwork.IsConnected) return;
        PhotonNetwork.ConnectUsingSettings();

    }

    private void ParamaterUpdate()
    {
        roomName = room_InputField.text;
        playerNum = (int)people_Slider.value;
        people_Num.text = playerNum.ToString();
        debug.text = PhotonNetwork.CountOfRooms.ToString();
    }

    private void RoomListReset()
    {

    }
}
