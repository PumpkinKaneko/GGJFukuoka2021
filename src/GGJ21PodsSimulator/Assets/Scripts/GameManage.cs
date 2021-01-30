using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    #endregion


    private void Start()
    {
        
    }


    private void Update()
    {
        
    }


    public void Entry ()
    {

    }


    public void Exit ()
    {

    }
}
