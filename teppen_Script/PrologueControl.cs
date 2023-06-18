using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueControl : MonoBehaviour
{
    // プレイヤーの状態
    private enum playerState
    {
        prologue = 0,
        main = 1,
    };

    // プレイヤーの状態を管理する構造体変数 (0:メンチを切り合うイベント　1:戦闘開始状態　)
    private playerState _playerstate = playerState.prologue;

    //【プレイヤーの情報】
    // 自作スクリプト
    // 自プレイヤーの playercontrol を管理する変数
    playercontrol _playercontrol;
    // 自プレイヤーの playerattack を管理する変数
    playerattack _playerattack;
    // 自プレイヤーの PrologueControl を管理する変数
    PrologueControl _prologueControl;

    // unityの機能
    // 自プレイヤーの Rigidbody を管理する変数
    Rigidbody _rb;
    // 自プレイヤーの アニメーター を管理する変数
    Animator _animator;
    // 自プレイヤーの 前方ベクトル を管理する変数
    Vector3 _fowardVector;

    //【敵の情報】
    // 敵の playercontrol の情報を管理する変数
    [SerializeField]private playercontrol _EnemyObj;
    // 敵オブジェクトの情報を管理する変数
    GameObject _prevEnemyObj;

    //【距離関係】
    // 相手との距離の情報を管理する
    Vector3 _distance;
    // 相手との距離を絶対値として管理する
    float _animDistance = 0.0f;
    // 相手との掴みイベントが発生する距離
    [SerializeField, Header("掴み合いイベントの発生距離")] float _stareEventStartDistance;

    //【イベント関係】
    // 最初の掴み合いイベントのフラグを管理する
    bool _starePrologueEventF = false;



    // Start is called before the first frame update
    void Start()
    {
        // 敵プレイヤーが null ではない場合
        if(_EnemyObj != null)
        {
            // 敵オブジェクトの情報を更新する
            _prevEnemyObj = _EnemyObj._getEnemyObj();

            //デバッグ用
            //Debug.Log("PrologueControl >> _prevEnemyObj：" + _prevEnemyObj);

        }

        // プレイヤーの情報を更新する
        if(_playercontrol == null)
        {
            _playercontrol = this.gameObject.GetComponent<playercontrol>();
            //デバッグ用
            //Debug.Log("PrologueControl >> _playercontrol：" + _playercontrol);
        }
        if (_playerattack == null)
        { 
            _playerattack = this.gameObject.GetComponent<playerattack>();
            //デバッグ用
            //Debug.Log("PrologueControl >> _playerattack：" + _playerattack);
        }
        if(_prologueControl == null)
        {
            _prologueControl = this.gameObject.GetComponent<PrologueControl>();
            //デバッグ用
            //Debug.Log("PrologueControl >> prologueControl：" + _prologueControl);
        }


        // プレイヤーの Rigidbody を取得する
        _rb = GetComponent<Rigidbody>();
        
        // プレイヤーの前方のベクトルを更新する
        _fowardVector = this.gameObject.transform.forward;

        //　プレイヤーに Animator を取得する
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
 
        //プレイヤーの歩きのアニメーションへ移行する
        _animator.SetFloat("_PrologueAnim", _animDistance);
        //デバッグ用
        //Debug.Log("PrologueControl >> _speed：" + _animDistance);

        switch (_playerstate)
        {
            // メンチを切り合うイベント中
            case playerState.prologue:
                Debug.Log("PrologueControl >> _playerstate：" + _playerstate);
                _playercontrol.enabled = false;      // 自プレイヤーのを playercontrol 無効にする
                _playerattack.enabled = false;       // 自プレイヤーのを playerattack 無効にする

                // プロローグの処理
                _prologue();

                break;

            // 戦闘中の状態
            case playerState.main:
                Debug.Log("PrologueControl >> _playerstate：" + _playerstate);

                _starePrologueEventF = false;       // 最初の掴み合いイベントを false にする

                _playercontrol.enabled = true;      // 自プレイヤーの playercontrol を有効にする
                _playerattack.enabled = true;       // 自プレイヤーの playerattack を有効にする
                _prologueControl.enabled = false;   // 自プレイヤーの PrologueControl を無効にする

                break;

        }
    }

    private void _prologue()
    {
        // 最初のメンチを切り合いが発生していない場合
        if (_starePrologueEventF == false)
        {
            // 自分と相手の距離を更新する処理
            _distance = this.gameObject.transform.position - _EnemyObj.transform.position;
            // 距離を絶対値にする
            _animDistance = Mathf.Abs(_distance.x);
            //デバッグ用
            //Debug.Log("PrologueControl >> " + this.gameObject.name +" >> "+ "_distance：" + _distance);

            // プレイヤーを移動させる処理
            _rb.velocity = new Vector3(_fowardVector.x * _playercontrol._getMovespeed(), _distance.y, _distance.z);
            //デバッグ用
            //Debug.Log("PrologueControl >> " + this.gameObject.name + " >> " + "_speed：" + _fowardVector.x * _playercontrol._getMovespeed());

            // プレイヤー同士が _stareEventStartDistance より近くにいた場合
            if (_animDistance <= _stareEventStartDistance)
            {
                // _starePrologueEventF を ON にする
                _starePrologueEventF = true;

                // 掴み合いのアニメーションへ移行する
                _animator.SetBool("_StareAttack", _starePrologueEventF);

                // 掴み合いイベントを開始する
                stareStagingControl._stareEvent_Start = true;
                // 歩かないように値を更新する
                _animDistance = -1.0f;

            }
        }
    }

    // 状態を切り替える
    public void setGameState(int _stateNum)
    {
        _playerstate = (playerState)_stateNum;
    }

}
