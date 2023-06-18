using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControlManager : MonoBehaviour
{
    //-------------------------------------
    // ����ҁF���c���l
    // ��ʑJ�ڂ��s���ۂ͕K���p�������鎖
    //-------------------------------------
    public enum Scene
    {
        title = 0,          // �^�C�g��
        synopsis,           // ���炷��
        modeselect,         // �Q�[�����[�h�I��
        training,           // �g���[�j���O���[�h�I��
        charaselect,        // �L�����N�^�[�I��
        main,               // �C���Q�[��
        result,             // ���U���g
        exit,               // �����I��
    };

    [SerializeField] SceneDataBase sceneDataBase;

    private string _sceneName;
    private bool _sceneChangeF;

    // start���\�b�h�̑O�ɌĂяo��
    private void Awake()
    {
        GameObject sceneManager = CheckOtherSceneManager();
        bool checkResult = sceneManager != null && sceneManager != gameObject;
        
        Time.timeScale = 1f;        // ���Ԃ̑��x��߂�(1.0f)

        if (checkResult)
        {
            Debug.Log("SceneControlManager�͊��ɑ��݂��Ă��܂�");
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

    //�Q�[���I��
    private void _EndGame()
    {
        if (_sceneName == sceneDataBase.SceneBaseList[(int)Scene.exit].sceneName)
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;        //�Q�[���v���C�I��
#else
        Application.Quit();//�Q�[���v���C�I��
#endif
        }
    }
}
