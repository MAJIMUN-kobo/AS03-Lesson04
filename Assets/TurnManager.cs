using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    // ターン内のフェイズ >>>>
    public const int PHASE_HAND_SELECT_P1 = 0;  // フェイズ：手の選択(1P)
    public const int PHASE_HAND_SELECT_P2 = 1;  // フェイズ：手の選択(2P)
    public const int PHASE_BATTLE         = 2;  // フェイズ：じゃんけん処理
    public const int PHASE_DAMAGE_CALC    = 3;  // フェイズ：ダメージ計算
    public const int PHASE_BATTLE_RESULT  = 4;  // フェイズ：結果表示
    public const int PHASE_ENTRY          = 5;  // フェイズ：ターン開始処理 
    //  <<< 勝敗が決まるまで上記を繰り返す

    public int turnCount = 0;           // ターン数
    public int phasePattern = 0;        // フェイズを回すための変数

    // === プレイヤー
    public Player[] player = null;


    // Start is called before the first frame update
    void Start()
    {
        phasePattern = PHASE_ENTRY;

        // === プレイヤー情報を初期化
        for(int i = 0; i < player.Length; i++) 
        {
            player[i].jankenHand = Player.HAND_GU;  // 手の初期化
        }
    }

    // Update is called once per frame
    void Update()
    {
        // === 
        /* >>> Swtch文の書き方 <<<
           swtch(条件)
           {
              case パターン:
                 >>> 処理
              break;
           }
        */
        switch( phasePattern )
        {
            case PHASE_ENTRY:
                this.TurnEntry();

                break;

            case PHASE_HAND_SELECT_P1:
                this.HandSelectP1();
                
                break;

            case PHASE_HAND_SELECT_P2:
                this.HandSelectP2();
                
                break;

            case PHASE_BATTLE:
                this.Battle();
                
                break;

            case PHASE_DAMAGE_CALC:
                this.DamageCalc();

                break;

            case PHASE_BATTLE_RESULT:
                this.Result();
                
                break;
        }
    }


    // === ターンの初期化
    public void TurnEntry()
    {
        Debug.Log("ターンを開始します。");

        turnCount++; // ターン数を増やします。

        // フェイズを次に進める
        phasePattern = PHASE_HAND_SELECT_P1;
    }

    // === P1：手の選択
    public void HandSelectP1()
    {
        Debug.Log("プレイヤー1 >> 手を選んでください。");

        if(Input.GetKeyDown(KeyCode.Alpha1) == true)
        {   // "1キー"でグーを選択
            player[0].jankenHand = Player.HAND_GU;

            Debug.Log("プレイヤー1 >> グーを出しました。");

            phasePattern = PHASE_HAND_SELECT_P2;    // フェイズを次に進める
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) == true)
        {   // "2キー"でチョキを選択
            player[0].jankenHand = Player.HAND_CHOKI;

            Debug.Log("プレイヤー1 >> チョキを出しました。");

            phasePattern = PHASE_HAND_SELECT_P2;    // フェイズを次に進める
        }

        if(Input.GetKeyDown(KeyCode.Alpha3) == true)
        {   // "3キー"でパーを選択
            player[0].jankenHand = Player.HAND_PA;

            Debug.Log("プレイヤー1 >> パーを出しました。");

            phasePattern = PHASE_HAND_SELECT_P2;    // フェイズを次に進める
        }
    }

    // === P2：手の選択
    public void HandSelectP2() 
    {
        Debug.Log("プレイヤー2 >> 手を選んでください。");

        if (Input.GetKeyDown(KeyCode.Alpha1) == true)
        {   // "1キー"でグーを選択
            player[1].jankenHand = Player.HAND_GU;

            Debug.Log("プレイヤー2 >> グーを出しました。");

            phasePattern = PHASE_BATTLE;    // フェイズを次に進める
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) == true)
        {   // "1キー"でチョキを選択
            player[1].jankenHand = Player.HAND_CHOKI;

            Debug.Log("プレイヤー2 >> チョキを出しました。");

            phasePattern = PHASE_BATTLE;    // フェイズを次に進める
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) == true)
        {   // "1キー"でパーを選択
            player[1].jankenHand = Player.HAND_PA;

            Debug.Log("プレイヤー2 >> パーを出しました。");

            phasePattern = PHASE_BATTLE;    // フェイズを次に進める
        }
    }

    // === バトル処理
    public void Battle()
    {
        Debug.Log("じゃんけんの判定を行います。");

        // フェイズを次に進める
        phasePattern = PHASE_DAMAGE_CALC;
    }

    // === ダメージ計算
    public void DamageCalc()
    {
        Debug.Log("ダメージ計算を行います。");

        // フェイズを次に進める
        phasePattern = PHASE_BATTLE_RESULT;
    }

    // === バトルリザルト
    public void Result()
    {
        Debug.Log("＊＊＊さんの勝ちです。ーーーさんに　???　のダメージ！");

        // フェイズを次に進める
        phasePattern = PHASE_ENTRY;
    }


    // ===
    // ログの表示（デバッグ用）
    // ===
    public void OnGUI()
    {
        GUILayout.Label("Turn >>> " + turnCount);
    }
}
