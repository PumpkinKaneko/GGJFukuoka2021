using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class SceneState
{
    #region プロパティ
    public string SceneName { get; protected set; }
    #endregion

    public SceneState()
    {

    }

    public abstract void Entry();
    public abstract void Execute();
    public abstract void Exit(SceneState next);
    public abstract void SceneLoaded(Scene next, LoadSceneMode mode = LoadSceneMode.Single);
}


public class BaseSceneState : SceneState
{
    protected GameManage _main;

    #region プロパティ
    public bool Loaded { get; set; }
    #endregion


    public BaseSceneState() : base()
    {

    }


    public override void Entry()
    {
        Debug.Log("シーン[" + this.SceneName + "]を実行します。");
    }


    public override void Execute()
    {

    }


    public override void Exit(SceneState next)
    {
        this.Loaded = false;

        Debug.Log(
            "シーン[" + this.SceneName + "]を終了します。"
            + "\n次のシーンは[" + next.SceneName + "]です。"
            );
    }


    public override void SceneLoaded(Scene next, LoadSceneMode mode = LoadSceneMode.Single)
    {
        Debug.Log("シーンが[" + SceneManager.GetActiveScene().name + "]に切り替わりました。");
        this.Loaded = true;

        SceneManager.sceneLoaded -= this.SceneLoaded;
    }
}


public class TitleScene : BaseSceneState
{
    private Button _multiPlayButton;


    public TitleScene(GameManage main, string sceneName)
    {
        _main = main;

        this.SceneName = sceneName;     // シーン名
    }


    public override void Entry()
    {
        base.Entry();

        
    }


    public override void Execute()
    {
        if (!this.Loaded) return;

        if (_multiPlayButton == null)
        {
            _multiPlayButton = GameObject.Find("Canvas/PlayButtonGroup/MultiPlayButton").GetComponent<Button>();
            _multiPlayButton.onClick.AddListener(() => {
                _main.LoadScene(new LobbyScene(_main, "Lobby"));
            });
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _main.LoadScene(new InGameScene(_main, "InGameScene"));
        }
    }


    public override void Exit(SceneState next)
    {
        base.Exit(next);

        next.Entry();
    }
}


public class InGameScene : BaseSceneState
{
    private Text _timeText;

    public float gameTime { get; private set; }


    public InGameScene(GameManage main, string sceneName):base()
    {
        _main = main;

        this.SceneName = sceneName;     // シーン名
    }

    public override void Entry()
    {
        base.Entry();

        _main.GameFinished = false;
        this.gameTime = GameManage.Instance.InGameTime;
    }


    public override void Execute()
    {
        if (!this.Loaded || !_main.IsMatched) return;

        if (_timeText == null)
            _timeText = GameObject.Find("Canvas/TimeText/Value").GetComponent<Text>();
        else
            _timeText.text = this.gameTime.ToString("N1");        

        this.gameTime -= Time.deltaTime;        // ゲーム時間
        if (this.gameTime < 0)
        {
            _main.GameFinished = true;
            _main.Winner = WinnerState.Pods;
            _main.LoadScene(new ResultScene(_main, "ResultScene"));
        }

        base.Execute();
    }


    public override void Exit(SceneState next)
    {
        base.Exit(next);

        next.Entry();
    }
}


public class LobbyScene : BaseSceneState
{
    public LobbyScene(GameManage main, string sceneName)
    {
        _main = main;

        this.SceneName = sceneName;     // シーン名
    }


    public override void Entry()
    {
        base.Entry();
    }

    public override void Execute()
    {
        
    }

    public override void Exit(SceneState next)
    {
        base.Exit(next);

        next.Entry();
    }
}


public class RoomScene : BaseSceneState
{
    public RoomScene(GameManage main, string sceneName)
    {
        _main = main;

        this.SceneName = sceneName;     // シーン名
    }


    public override void Entry()
    {
        base.Entry();
    }

    public override void Execute()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            _main.LoadScene(new InGameScene(_main, "InGameScene"));
        }
    }

    public override void Exit(SceneState next)
    {
        base.Exit(next);

        next.Entry();
    }
}


public class ResultScene : BaseSceneState
{
    private Button _backLobbyButton;
    private Text _winnerText;
    private GameObject _winnerObject;
    private GameObject _loserObject;
    private string _playerName = "TitleHuman";
    private string _podsName = "AirPodsPro";


    public ResultScene(GameManage main, string sceneName)
    {
        _main = main;

        this.SceneName = sceneName;     // シーン名
    }


    public override void Entry()
    {
        base.Entry();
    }

    public override void Execute()
    {
        if (!this.Loaded) return;

        if (_backLobbyButton == null)
        {   // ボタンで画面遷移(-> Lobby)
            _backLobbyButton = GameObject.Find("Canvas/ResultButtonGroup/BackLobbyButton").GetComponent<Button>();
            _backLobbyButton.onClick.AddListener(this.GotoScene);
        }


        // 勝敗
        if(_loserObject == null || _winnerObject == null)
        {
            GetWinnter(_main.Winner);
        }
        else
        {
            if (_winnerText == null)
            {
                _winnerText = GameObject.Find("Canvas/WinnerText").GetComponent<Text>();
                _winnerText.text = _winnerObject.name + "\nWin";
            }
        }

    }

    public override void Exit(SceneState next)
    {
        base.Exit(next);

        next.Entry();
    }


    public void GetWinnter (WinnerState state)
    {
        switch(state)
        {
            case WinnerState.None:
                break;

            case WinnerState.Player:
                _winnerObject = GameObject.Find(_playerName);
                _loserObject = GameObject.Find(_podsName);
                break;

            case WinnerState.Pods:
                _winnerObject = GameObject.Find(_podsName);
                _loserObject = GameObject.Find(_playerName);
                break;
        }
    }


    public void GotoScene()
    {
        _main.LoadScene(new LobbyScene(_main, "Lobby"));
    }
}
