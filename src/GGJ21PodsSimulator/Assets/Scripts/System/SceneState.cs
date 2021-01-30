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

        Debug.Log(GameManage.Instance.CurrentScene);

        SceneManager.sceneLoaded -= this.SceneLoaded;
    }
}


public class TitleScene : BaseSceneState
{
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

        if(Input.GetKeyDown(KeyCode.Space))
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
        if (this.gameTime < _main.InGameTime)
        {
            //_main.LoadScene(new ResultScene(_main, "ResultScene"));
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

    }

    public override void Exit(SceneState next)
    {
        base.Exit(next);

        next.Entry();
    }
}


public class ResultScene : BaseSceneState
{
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
        
    }

    public override void Exit(SceneState next)
    {
        base.Exit(next);

        next.Entry();
    }
}
