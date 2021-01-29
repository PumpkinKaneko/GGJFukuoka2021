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
    private string roomName = "";
    private int playerNum = 0;
    private List<RoomInfo> roomDispList;
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
        PhotonNetwork.JoinOrCreateRoom(room_InputField.text, roomOptions, TypedLobby.Default);
        SceneManager.LoadScene("Main");
    }

    public void ListOpen()
    {
        if(listWindow.activeSelf == false)
        listWindow.SetActive(true);
    }

    public void ListClose()
    {
        if (listWindow.activeSelf)
            listWindow.SetActive(false);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate");
        base.OnRoomListUpdate(roomList);
        foreach (var info in roomList)
        {
            if (!info.RemovedFromList)
            {
                roomDispList.Add(info);
            }
            else
            {
                roomDispList.Remove(info);
            }
        }
    }

    private void Init()
    {
        roomDispList = new List<RoomInfo>();
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
        debug.text = roomDispList.Count.ToString();
    }

    private void RoomListReset()
    {

    }
}
