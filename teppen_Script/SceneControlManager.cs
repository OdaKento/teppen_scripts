using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControlManager : MonoBehaviour
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
        result,             // リザルト
        exit,               // 強制終了
    };

    [SerializeField] SceneDataBase sceneDataBase;

    private string _sceneName;
    private bool _sceneChangeF;

    // startメソッドの前に呼び出す
    private void Awake()
    {
        GameObject sceneManager = CheckOtherSceneManager();
        bool checkResult = sceneManager != null && sceneManager != gameObject;
        
        Time.timeScale = 1f;        // 時間の速度を戻す(1.0f)

        if (checkResult)
        {
            Debug.Log("SceneControlManagerは既に存在しています");
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    GameObject CheckOtherSceneManager()
    {
        return GameObject.FindGameObjectWithTag("SceneManager");
    }

    public void changeScene(string _scenename,float _scenetime = 0.0f)
    {
        _sceneName = _scenename;
        Invoke("SceneLoad", _scenetime);
    }

    private void SceneLoad()
    {
        _EndGame();

        if (!_sceneChangeF)
        {

            _sceneChangeF = true;
            SceneManager.LoadScene(_sceneName);
        }
    }

    //ゲーム終了
    private void _EndGame()
    {
        if (_sceneName == sceneDataBase.SceneBaseList[(int)Scene.exit].sceneName)
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;        //ゲームプレイ終了
#else
        Application.Quit();//ゲームプレイ終了
#endif
        }
    }
}
