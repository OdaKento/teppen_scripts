using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{
    //-------------------------------------
    // 制作者：小田健人
    // 画面遷移を行う際は必ず継承させる事
    //-------------------------------------
    public enum Scene
    {
        title = 0,          // タイトル
        synopsis,           // あらすじ
        modeselect,         // ゲームモード選択
        training,           // トレーニングモード選択
        charaselect,        // キャラクター選択
        main,               // インゲーム
        result,              // リザルト
        exit
    };

    [SerializeField, Header("シーン管理")] public Scene[] _SceneType;
    [SerializeField, Header("シーン遷移のインターバル")] private float changeTime;

    public static int sceneNumber;               // 
    public static float _changeSceneTime;        // 

    private int prevsceneNumber;                 // 

    private void Start()
    {
        Application.targetFrameRate = 60;
        _changeSceneTime = changeTime;
    }

}
