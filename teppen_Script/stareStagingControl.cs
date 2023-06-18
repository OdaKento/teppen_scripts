using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stareStagingControl : MonoBehaviour
{
    //【inspector上で表示する変数】
    [Header("プレイヤー1データ")]
    [SerializeField] GameObject player1;                    // プレイヤー1のオブジェクトの情報を管理する
    [SerializeField] Text player1_Consecutive_text;         // プレイヤー1の連打数を表示するテキスト
    //【inspector上で非表示にする変数】
    private playercontrol player1_control;                  // プレイヤー1のを playercontrol 管理する
    private playerattack player1_attack;                    // プレイヤー1のを playerattack 管理する
    private PlayerHP player1HP_Obj;                         // プレイヤー1のを PlayerHP 管理する
    private PrologueControl player1_prologue;               // プレイヤー1のを PrologueControl 管理する

    //【inspector上で表示する変数】
    [Header("プレイヤー2データ")]
    [SerializeField] GameObject player2;                    // プレイヤー2のオブジェクトの情報を管理する
    [SerializeField] Text player2_Consecutive_text;         // プレイヤー2の連打数を表示するテキスト
    //【inspector上で非表示にする変数】
    private playercontrol player2_control;                  // プレイヤー2のを playercontrol 管理する
    private playerattack player2_attack;                    // プレイヤー2のを playerattack 管理する
    private PlayerHP player2HP_Obj;                         // プレイヤー2のを PlayerHP 管理する
    private PrologueControl player2_prologue;               // プレイヤー2のを PrologueControl 管理する


    private int _prevPlayer1_count;                         // プレイヤー1の入力回数を管理する変数
    private int _prevPlayer2_count;                         // プレイヤー2の入力回数を管理する変数

    [Header("イベント終了時間データ")]
    [SerializeField] private int maxEventTimer;
    private float prevTimer;
    [SerializeField] private Text EventTime_text;


    // つかみ合い発生中の状態を管理する
    [SerializeField] private bool _StareF;

    // つかみ合いイベント発生開始時のフラグを管理する
    public static bool _stareEvent_Start;

    // Start is called before the first frame update
    void Start()
    {
        // player1 が null ではない場合
        if (player1 != null)
        {
            player1HP_Obj = player1.GetComponent<PlayerHP>();               // player1 の PlayerHP を取得する
            player1_control = player1.GetComponent<playercontrol>();        // player1 の playercontrol を取得する
            player1_attack = player1.GetComponent<playerattack>();          // player1 の playerattack を取得する
            player1_prologue = player1.GetComponent<PrologueControl>();     // player1 の PrologueControl を取得する
        }

        // player2 が null ではない場合
        if (player2 != null)
        {
            player2HP_Obj = player2.GetComponent<PlayerHP>();               // player2 の PlayerHP を取得する
            player2_control = player2.GetComponent<playercontrol>();        // player2 の playercontrol を取得する
            player2_attack = player2.GetComponent<playerattack>();          // player2 の playerattack を取得する
            player2_prologue = player2.GetComponent<PrologueControl>();     // player2 の PrologueControl を取得する
        }

    }

    // Update is called once per frame
    void Update()
    {
        // つかみ合いイベントが開始した時
        if (_stareEvent_Start)
        {
            // つかみ合い開始
            _StareF = true;
            // つかみ合いイベント開始フラグを切る
            stareStagingControl._stareEvent_Start = false;
            Debug.Log("stareStagingControl >> _StareF：" + _StareF);
            prevTimer = maxEventTimer;
            Debug.Log("stareStagingControl >> maxEventTimer：" + maxEventTimer);

            player1_control.enabled = false;
            player2_control.enabled = false;
        }

        // テキストの表示・非表示
        {
            EventTime_text.enabled = _StareF;                 // イベント終了時間テキストの表示・非表示  
            player1_Consecutive_text.enabled = _StareF;       // プレイヤー1連打数テキストの表示・非表示
            player2_Consecutive_text.enabled = _StareF;       // プレイヤー2連打数テキストの表示・非表示
        }

        if (_StareF)
        {
            // イベント時間を更新する
            prevTimer -= Time.deltaTime;
            int textTime = (int)prevTimer;
            EventTime_text.text = textTime.ToString();

            // プレイヤー1の連打数を更新する処理
            if (_prevPlayer1_count != player1_attack._getConsecutive())
            {
                _prevPlayer1_count = player1_attack._getConsecutive();
                player1_Consecutive_text.text = _prevPlayer1_count.ToString();
                //Debug.Log("stareStagingControl >> _prevPlayer1_count：" + _prevPlayer1_count);
            }

            //プレイヤー2の連打数を更新する処理
            if (_prevPlayer2_count != player2_attack._getConsecutive())
            {
                _prevPlayer2_count = player2_attack._getConsecutive();
                player2_Consecutive_text.text = _prevPlayer2_count.ToString();
                //Debug.Log("stareStagingControl >> _prevPlayer2_count：" + _prevPlayer2_count);
            }

            if (prevTimer < 0)
            {
                // 勝敗判定を呼び出す
                Event_Victory_Judgment();

                player1_attack.resetConsecutive();   // プレイヤー1の連打数をリセットする
                player2_attack.resetConsecutive();   // プレイヤー2の連打数をリセットする
                _StareF = false;

                if (player1_prologue != null)
                { player1_prologue.setGameState(1); }   // プレイヤーの状態を main にする
                if (player1_prologue != null)
                { player2_prologue.setGameState(1); }   // プレイヤーの状態を main にする

                player1_control.enabled = true;
                player2_control.enabled = true;
            }
        }
    }

    // 掴み合いイベントの勝敗判定
    public void Event_Victory_Judgment()
    {
        if (_prevPlayer1_count == _prevPlayer2_count)
        {
            Debug.Log("stareStagingControl >> victoryJudgment：" + "引き分け");
            return;
        }
        else if (_prevPlayer1_count < _prevPlayer2_count)
        {
            Debug.Log("stareStagingControl >> victoryJudgment：" + "プレイヤー2の勝ち");
            player1HP_Obj._HitStareAttack = true;
        }
        else if (_prevPlayer1_count > _prevPlayer2_count)
        {
            Debug.Log("stareStagingControl >> victoryJudgment：" + "プレイヤー1の勝ち");
            player2HP_Obj._HitStareAttack = true;
        }
    }


    public bool getStare()
    {
        return _StareF;
    }
}
