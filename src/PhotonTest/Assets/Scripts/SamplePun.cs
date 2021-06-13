using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SamplePun : MonoBehaviourPunCallbacks
{
    private MonsterScript player;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.IsMessageQueueRunning = true;
        GameObject monster = PhotonNetwork.Instantiate("monster", Vector3.zero, Quaternion.identity, 0);
        DontDestroyOnLoad(monster);
        MonsterScript monsterScript = monster.GetComponent<MonsterScript>();
        monsterScript.enabled = true;
        var view = monster.GetComponent<PhotonView>();
        monsterScript.Init(view.ViewID.ToString());
        player = monsterScript;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        GUI.skin.label.fontSize = 30;
        GUI.Label(new Rect(100, 180, 500, 100), "Name: " + player._name);
    }
}

