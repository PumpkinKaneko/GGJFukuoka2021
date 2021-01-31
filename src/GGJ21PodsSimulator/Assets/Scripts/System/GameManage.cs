using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

// 勝敗・ステージプログラム・シーン管理・音量管理(オプション)[低]
// 勝敗：
// タイトル　Robi　ゲーム　リザルト→Robi
// ロールの割当・人1　残エア

public class GameManage : SingletonMonoBehaviour<GameManage>
{
    public class PlayerDetail
    {
        public string[] UserName;   // 
        public int[] coloer;        //
    }

    [Header("Start Settings")]
    public string StartSceneName = "";

    [Header("InGame Options")]
    public float InGameTime;

    // プレイヤー情報[ルーム(待合室)で確定]
    public PlayerDetail[] PlayerDetails;


    #region プロパティ
    public SceneState CurrentScene { get; private set; }
    public SceneState PrevScene { get; private set; }
    public WinnerState Winner { get; private set; }
    public bool IsMatched { get; set; }
    public bool GameFinished { get; set; }
    // ロビー名[ルーム(待合室)で確定]
    public string LobbyName { get; set; }
    #endregion


    private void Start()
    {
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(this.gameObject);

        if (string.IsNullOrEmpty(this.StartSceneName)) this.StartSceneName = SceneManager.GetActiveScene().name;

        this.IsMatched = false;
        this.CurrentScene = this.GetSceneState(this.StartSceneName);
        this.LoadScene(this.CurrentScene);
    }


    private void Update()
    {
        if (this.CurrentScene != null)
        {
            this.CurrentScene.Execute();        // シーン内処理の実行
        }
    }


    public void ChangeSceneState (SceneState next)
    {
        if (this.CurrentScene != null)
        {
            this.CurrentScene.Exit(next);
            this.PrevScene = this.CurrentScene;
        }

        this.CurrentScene = next;
    }


    public void LoadScene (SceneState next, LoadSceneMode mode = LoadSceneMode.Single)
    {
        this.ChangeSceneState(next);

        if (this.CurrentScene != null)
        {
            SceneManager.sceneLoaded += this.CurrentScene.SceneLoaded;
        }

        SceneManager.LoadScene(next.SceneName, mode);
    }


    public AsyncOperation LoadSceneAsync (SceneState next, LoadSceneMode mode = LoadSceneMode.Single)
    {
        this.ChangeSceneState(next);

        if (this.CurrentScene != null)
        {
            SceneManager.sceneLoaded += this.CurrentScene.SceneLoaded;
        }

        return SceneManager.LoadSceneAsync(next.SceneName, mode);
    }


    public SceneState GetSceneState(string sceneName)
    {
        SceneState state = null;

        switch(sceneName)
        {
            case "TitleScene":
                state = new TitleScene(this, sceneName);

                break;

            case "Lobby":
                state = new LobbyScene(this, sceneName);

                break;

            case "InGameScene":
                state = new InGameScene(this, sceneName);

                break;

            case "WaitRoom":
                state = new RoomScene(this, sceneName);

                break;

            case "ResultScene":
                state = new ResultScene(this, sceneName);

                break;
        }

        return state;
    }
}


public enum WinnerState
{
    None = 0,
    Player = 1,
    Pods = 2
}
