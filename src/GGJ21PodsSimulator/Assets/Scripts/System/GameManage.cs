﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 勝敗・ステージプログラム・シーン管理・音量管理(オプション)[低]
// 勝敗：
// タイトル　Robi　ゲーム　リザルト→Robi
// ロールの割当・人1　残エア

public class GameManage : SingletonMonoBehaviour<GameManage>
{
    [Header("InGame Options")]
    public Text InGaemTimeUI;
    public float InGameTime;

    private List<GameObject> _photonToken;
    private float _gameTime;


    #region プロパティ
    public SceneState CurrentScene { get; private set; }
    public SceneState PrevScene { get; private set; }
    #endregion


    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        this.CurrentScene = new TitleScene(this, "TitleScene");
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


    
}
