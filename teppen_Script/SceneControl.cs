using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
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
        result,              // ���U���g
        exit
    };

    [SerializeField, Header("�V�[���Ǘ�")] public Scene[] _SceneType;
    [SerializeField, Header("�V�[���J�ڂ̃C���^�[�o��")] private float changeTime;

    public static int sceneNumber;               // 
    public static float _changeSceneTime;        // 

    private int prevsceneNumber;                 // 

    private void Start()
    {
        Application.targetFrameRate = 60;
        _changeSceneTime = changeTime;
    }

}
