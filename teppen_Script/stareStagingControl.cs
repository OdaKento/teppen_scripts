using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stareStagingControl : MonoBehaviour
{
    //�yinspector��ŕ\������ϐ��z
    [Header("�v���C���[1�f�[�^")]
    [SerializeField] GameObject player1;                    // �v���C���[1�̃I�u�W�F�N�g�̏����Ǘ�����
    [SerializeField] Text player1_Consecutive_text;         // �v���C���[1�̘A�Ő���\������e�L�X�g
    //�yinspector��Ŕ�\���ɂ���ϐ��z
    private playercontrol player1_control;                  // �v���C���[1�̂� playercontrol �Ǘ�����
    private playerattack player1_attack;                    // �v���C���[1�̂� playerattack �Ǘ�����
    private PlayerHP player1HP_Obj;                         // �v���C���[1�̂� PlayerHP �Ǘ�����
    private PrologueControl player1_prologue;               // �v���C���[1�̂� PrologueControl �Ǘ�����

    //�yinspector��ŕ\������ϐ��z
    [Header("�v���C���[2�f�[�^")]
    [SerializeField] GameObject player2;                    // �v���C���[2�̃I�u�W�F�N�g�̏����Ǘ�����
    [SerializeField] Text player2_Consecutive_text;         // �v���C���[2�̘A�Ő���\������e�L�X�g
    //�yinspector��Ŕ�\���ɂ���ϐ��z
    private playercontrol player2_control;                  // �v���C���[2�̂� playercontrol �Ǘ�����
    private playerattack player2_attack;                    // �v���C���[2�̂� playerattack �Ǘ�����
    private PlayerHP player2HP_Obj;                         // �v���C���[2�̂� PlayerHP �Ǘ�����
    private PrologueControl player2_prologue;               // �v���C���[2�̂� PrologueControl �Ǘ�����


    private int _prevPlayer1_count;                         // �v���C���[1�̓��͉񐔂��Ǘ�����ϐ�
    private int _prevPlayer2_count;                         // �v���C���[2�̓��͉񐔂��Ǘ�����ϐ�

    [Header("�C�x���g�I�����ԃf�[�^")]
    [SerializeField] private int maxEventTimer;
    private float prevTimer;
    [SerializeField] private Text EventTime_text;


    // ���ݍ����������̏�Ԃ��Ǘ�����
    [SerializeField] private bool _StareF;

    // ���ݍ����C�x���g�����J�n���̃t���O���Ǘ�����
    public static bool _stareEvent_Start;

    // Start is called before the first frame update
    void Start()
    {
        // player1 �� null �ł͂Ȃ��ꍇ
        if (player1 != null)
        {
            player1HP_Obj = player1.GetComponent<PlayerHP>();               // player1 �� PlayerHP ���擾����
            player1_control = player1.GetComponent<playercontrol>();        // player1 �� playercontrol ���擾����
            player1_attack = player1.GetComponent<playerattack>();          // player1 �� playerattack ���擾����
            player1_prologue = player1.GetComponent<PrologueControl>();     // player1 �� PrologueControl ���擾����
        }

        // player2 �� null �ł͂Ȃ��ꍇ
        if (player2 != null)
        {
            player2HP_Obj = player2.GetComponent<PlayerHP>();               // player2 �� PlayerHP ���擾����
            player2_control = player2.GetComponent<playercontrol>();        // player2 �� playercontrol ���擾����
            player2_attack = player2.GetComponent<playerattack>();          // player2 �� playerattack ���擾����
            player2_prologue = player2.GetComponent<PrologueControl>();     // player2 �� PrologueControl ���擾����
        }

    }

    // Update is called once per frame
    void Update()
    {
        // ���ݍ����C�x���g���J�n������
        if (_stareEvent_Start)
        {
            // ���ݍ����J�n
            _StareF = true;
            // ���ݍ����C�x���g�J�n�t���O��؂�
            stareStagingControl._stareEvent_Start = false;
            Debug.Log("stareStagingControl >> _StareF�F" + _StareF);
            prevTimer = maxEventTimer;
            Debug.Log("stareStagingControl >> maxEventTimer�F" + maxEventTimer);

            player1_control.enabled = false;
            player2_control.enabled = false;
        }

        // �e�L�X�g�̕\���E��\��
        {
            EventTime_text.enabled = _StareF;                 // �C�x���g�I�����ԃe�L�X�g�̕\���E��\��  
            player1_Consecutive_text.enabled = _StareF;       // �v���C���[1�A�Ő��e�L�X�g�̕\���E��\��
            player2_Consecutive_text.enabled = _StareF;       // �v���C���[2�A�Ő��e�L�X�g�̕\���E��\��
        }

        if (_StareF)
        {
            // �C�x���g���Ԃ��X�V����
            prevTimer -= Time.deltaTime;
            int textTime = (int)prevTimer;
            EventTime_text.text = textTime.ToString();

            // �v���C���[1�̘A�Ő����X�V���鏈��
            if (_prevPlayer1_count != player1_attack._getConsecutive())
            {
                _prevPlayer1_count = player1_attack._getConsecutive();
                player1_Consecutive_text.text = _prevPlayer1_count.ToString();
                //Debug.Log("stareStagingControl >> _prevPlayer1_count�F" + _prevPlayer1_count);
            }

            //�v���C���[2�̘A�Ő����X�V���鏈��
            if (_prevPlayer2_count != player2_attack._getConsecutive())
            {
                _prevPlayer2_count = player2_attack._getConsecutive();
                player2_Consecutive_text.text = _prevPlayer2_count.ToString();
                //Debug.Log("stareStagingControl >> _prevPlayer2_count�F" + _prevPlayer2_count);
            }

            if (prevTimer < 0)
            {
                // ���s������Ăяo��
                Event_Victory_Judgment();

                player1_attack.resetConsecutive();   // �v���C���[1�̘A�Ő������Z�b�g����
                player2_attack.resetConsecutive();   // �v���C���[2�̘A�Ő������Z�b�g����
                _StareF = false;

                if (player1_prologue != null)
                { player1_prologue.setGameState(1); }   // �v���C���[�̏�Ԃ� main �ɂ���
                if (player1_prologue != null)
                { player2_prologue.setGameState(1); }   // �v���C���[�̏�Ԃ� main �ɂ���

                player1_control.enabled = true;
                player2_control.enabled = true;
            }
        }
    }

    // �͂ݍ����C�x���g�̏��s����
    public void Event_Victory_Judgment()
    {
        if (_prevPlayer1_count == _prevPlayer2_count)
        {
            Debug.Log("stareStagingControl >> victoryJudgment�F" + "��������");
            return;
        }
        else if (_prevPlayer1_count < _prevPlayer2_count)
        {
            Debug.Log("stareStagingControl >> victoryJudgment�F" + "�v���C���[2�̏���");
            player1HP_Obj._HitStareAttack = true;
        }
        else if (_prevPlayer1_count > _prevPlayer2_count)
        {
            Debug.Log("stareStagingControl >> victoryJudgment�F" + "�v���C���[1�̏���");
            player2HP_Obj._HitStareAttack = true;
        }
    }


    public bool getStare()
    {
        return _StareF;
    }
}
