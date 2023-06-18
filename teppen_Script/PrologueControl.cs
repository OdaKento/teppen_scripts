using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueControl : MonoBehaviour
{
    // �v���C���[�̏��
    private enum playerState
    {
        prologue = 0,
        main = 1,
    };

    // �v���C���[�̏�Ԃ��Ǘ�����\���̕ϐ� (0:�����`��؂荇���C�x���g�@1:�퓬�J�n��ԁ@)
    private playerState _playerstate = playerState.prologue;

    //�y�v���C���[�̏��z
    // ����X�N���v�g
    // ���v���C���[�� playercontrol ���Ǘ�����ϐ�
    playercontrol _playercontrol;
    // ���v���C���[�� playerattack ���Ǘ�����ϐ�
    playerattack _playerattack;
    // ���v���C���[�� PrologueControl ���Ǘ�����ϐ�
    PrologueControl _prologueControl;

    // unity�̋@�\
    // ���v���C���[�� Rigidbody ���Ǘ�����ϐ�
    Rigidbody _rb;
    // ���v���C���[�� �A�j���[�^�[ ���Ǘ�����ϐ�
    Animator _animator;
    // ���v���C���[�� �O���x�N�g�� ���Ǘ�����ϐ�
    Vector3 _fowardVector;

    //�y�G�̏��z
    // �G�� playercontrol �̏����Ǘ�����ϐ�
    [SerializeField]private playercontrol _EnemyObj;
    // �G�I�u�W�F�N�g�̏����Ǘ�����ϐ�
    GameObject _prevEnemyObj;

    //�y�����֌W�z
    // ����Ƃ̋����̏����Ǘ�����
    Vector3 _distance;
    // ����Ƃ̋������Βl�Ƃ��ĊǗ�����
    float _animDistance = 0.0f;
    // ����Ƃ̒͂݃C�x���g���������鋗��
    [SerializeField, Header("�͂ݍ����C�x���g�̔�������")] float _stareEventStartDistance;

    //�y�C�x���g�֌W�z
    // �ŏ��̒͂ݍ����C�x���g�̃t���O���Ǘ�����
    bool _starePrologueEventF = false;



    // Start is called before the first frame update
    void Start()
    {
        // �G�v���C���[�� null �ł͂Ȃ��ꍇ
        if(_EnemyObj != null)
        {
            // �G�I�u�W�F�N�g�̏����X�V����
            _prevEnemyObj = _EnemyObj._getEnemyObj();

            //�f�o�b�O�p
            //Debug.Log("PrologueControl >> _prevEnemyObj�F" + _prevEnemyObj);

        }

        // �v���C���[�̏����X�V����
        if(_playercontrol == null)
        {
            _playercontrol = this.gameObject.GetComponent<playercontrol>();
            //�f�o�b�O�p
            //Debug.Log("PrologueControl >> _playercontrol�F" + _playercontrol);
        }
        if (_playerattack == null)
        { 
            _playerattack = this.gameObject.GetComponent<playerattack>();
            //�f�o�b�O�p
            //Debug.Log("PrologueControl >> _playerattack�F" + _playerattack);
        }
        if(_prologueControl == null)
        {
            _prologueControl = this.gameObject.GetComponent<PrologueControl>();
            //�f�o�b�O�p
            //Debug.Log("PrologueControl >> prologueControl�F" + _prologueControl);
        }


        // �v���C���[�� Rigidbody ���擾����
        _rb = GetComponent<Rigidbody>();
        
        // �v���C���[�̑O���̃x�N�g�����X�V����
        _fowardVector = this.gameObject.transform.forward;

        //�@�v���C���[�� Animator ���擾����
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
 
        //�v���C���[�̕����̃A�j���[�V�����ֈڍs����
        _animator.SetFloat("_PrologueAnim", _animDistance);
        //�f�o�b�O�p
        //Debug.Log("PrologueControl >> _speed�F" + _animDistance);

        switch (_playerstate)
        {
            // �����`��؂荇���C�x���g��
            case playerState.prologue:
                Debug.Log("PrologueControl >> _playerstate�F" + _playerstate);
                _playercontrol.enabled = false;      // ���v���C���[�̂� playercontrol �����ɂ���
                _playerattack.enabled = false;       // ���v���C���[�̂� playerattack �����ɂ���

                // �v�����[�O�̏���
                _prologue();

                break;

            // �퓬���̏��
            case playerState.main:
                Debug.Log("PrologueControl >> _playerstate�F" + _playerstate);

                _starePrologueEventF = false;       // �ŏ��̒͂ݍ����C�x���g�� false �ɂ���

                _playercontrol.enabled = true;      // ���v���C���[�� playercontrol ��L���ɂ���
                _playerattack.enabled = true;       // ���v���C���[�� playerattack ��L���ɂ���
                _prologueControl.enabled = false;   // ���v���C���[�� PrologueControl �𖳌��ɂ���

                break;

        }
    }

    private void _prologue()
    {
        // �ŏ��̃����`��؂荇�����������Ă��Ȃ��ꍇ
        if (_starePrologueEventF == false)
        {
            // �����Ƒ���̋������X�V���鏈��
            _distance = this.gameObject.transform.position - _EnemyObj.transform.position;
            // �������Βl�ɂ���
            _animDistance = Mathf.Abs(_distance.x);
            //�f�o�b�O�p
            //Debug.Log("PrologueControl >> " + this.gameObject.name +" >> "+ "_distance�F" + _distance);

            // �v���C���[���ړ������鏈��
            _rb.velocity = new Vector3(_fowardVector.x * _playercontrol._getMovespeed(), _distance.y, _distance.z);
            //�f�o�b�O�p
            //Debug.Log("PrologueControl >> " + this.gameObject.name + " >> " + "_speed�F" + _fowardVector.x * _playercontrol._getMovespeed());

            // �v���C���[���m�� _stareEventStartDistance ���߂��ɂ����ꍇ
            if (_animDistance <= _stareEventStartDistance)
            {
                // _starePrologueEventF �� ON �ɂ���
                _starePrologueEventF = true;

                // �͂ݍ����̃A�j���[�V�����ֈڍs����
                _animator.SetBool("_StareAttack", _starePrologueEventF);

                // �͂ݍ����C�x���g���J�n����
                stareStagingControl._stareEvent_Start = true;
                // �����Ȃ��悤�ɒl���X�V����
                _animDistance = -1.0f;

            }
        }
    }

    // ��Ԃ�؂�ւ���
    public void setGameState(int _stateNum)
    {
        _playerstate = (playerState)_stateNum;
    }

}
