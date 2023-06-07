using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

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

    // === エフェクト系
    [Header("=== エフェクト設定 ===")]
    public GameObject[] effectArray = null;     // エフェクトの配列
    public GameObject currentEffect = null;     // 現在出しているエフェクト

    // === サウンド系
    [Header("=== サウンド設定 ===")]
    public AudioClip[] seArray = null;      // SEの配列
    public float seVolume = 1;              // SEの音量
    public AudioClip[] bgmArray = null;     // BGMの配列
    public float bgmVolume = 1;             // BGMの音量


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

        // P1を初期化
        player[0].isWinner = false;

        // P2を初期化
        player[1].isWinner = false;

        // フェイズを次に進める
        phasePattern = PHASE_HAND_SELECT_P1;

        // プレイヤー1から選択中のエフェクトを出す
        Vector3 effectPos = player[0].gameObject.transform.position;
        currentEffect = Instantiate(effectArray[0], effectPos, Quaternion.Euler(-90, 0, 0), player[0].transform);

        // BGMを再生
        this.GetComponent<AudioSource>().clip = bgmArray[0];
        //this.GetComponent<AudioSource>().loop = true;
        this.GetComponent<AudioSource>().Play();
    }

    // === P1：手の選択
    public void HandSelectP1()
    {
        Debug.Log("プレイヤー1 >> 手を選んでください。");

        if (Input.GetKeyDown(KeyCode.Alpha1) == true)
        {   // "1キー"でグーを選択
            player[0].jankenHand = Player.HAND_GU;

            Debug.Log("プレイヤー1 >> グーを出しました。");

            phasePattern = PHASE_HAND_SELECT_P2;    // フェイズを次に進める

            // エフェクトを削除
            Destroy(currentEffect.gameObject);

            // プレイヤー2からエフェクトを出す
            Vector3 effectPos = player[1].transform.position;
            currentEffect = Instantiate(effectArray[0], effectPos, Quaternion.Euler(-90, 0, 0));

            // プレイヤー1からSEを再生する
            player[0].GetComponent<AudioSource>().PlayOneShot(seArray[0]);

            // BGMを変える
            this.GetComponent<AudioSource>().clip = bgmArray[1];
            this.GetComponent<AudioSource>().Play();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) == true)
        {   // "2キー"でチョキを選択
            player[0].jankenHand = Player.HAND_CHOKI;

            Debug.Log("プレイヤー1 >> チョキを出しました。");

            phasePattern = PHASE_HAND_SELECT_P2;    // フェイズを次に進める

            // エフェクトを削除
            Destroy(currentEffect.gameObject);

            // プレイヤー2からエフェクトを出す
            Vector3 effectPos = player[1].transform.position;
            currentEffect = Instantiate(effectArray[0], effectPos, Quaternion.Euler(-90, 0, 0));

            // プレイヤー1からSEを再生する
            player[0].GetComponent<AudioSource>().PlayOneShot(seArray[0]);
        }

        if(Input.GetKeyDown(KeyCode.Alpha3) == true)
        {   // "3キー"でパーを選択
            player[0].jankenHand = Player.HAND_PA;

            Debug.Log("プレイヤー1 >> パーを出しました。");

            phasePattern = PHASE_HAND_SELECT_P2;    // フェイズを次に進める

            // エフェクトを削除
            Destroy(currentEffect.gameObject);

            // プレイヤー2からエフェクトを出す
            Vector3 effectPos = player[1].transform.position;
            currentEffect = Instantiate(effectArray[0], effectPos, Quaternion.Euler(-90, 0, 0));

            // プレイヤー1からSEを再生する
            player[0].GetComponent<AudioSource>().PlayOneShot(seArray[0]);
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

            // エフェクトを削除
            Destroy(currentEffect.gameObject);

            // プレイヤー2からSEを再生する
            player[1].GetComponent<AudioSource>().PlayOneShot(seArray[0]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) == true)
        {   // "1キー"でチョキを選択
            player[1].jankenHand = Player.HAND_CHOKI;

            Debug.Log("プレイヤー2 >> チョキを出しました。");

            phasePattern = PHASE_BATTLE;    // フェイズを次に進める

            // エフェクトを削除
            Destroy(currentEffect.gameObject);

            // プレイヤー2からSEを再生する
            player[1].GetComponent<AudioSource>().PlayOneShot(seArray[0]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) == true)
        {   // "1キー"でパーを選択
            player[1].jankenHand = Player.HAND_PA;

            Debug.Log("プレイヤー2 >> パーを出しました。");

            phasePattern = PHASE_BATTLE;    // フェイズを次に進める

            // エフェクトを削除
            Destroy(currentEffect.gameObject);

            // プレイヤー2からSEを再生する
            player[1].GetComponent<AudioSource>().PlayOneShot(seArray[0]);
        }
    }

    // === バトル処理
    public void Battle()
    {
        Debug.Log("じゃんけんの判定を行います。");

        // 1Pが勝つパターン
        if (player[0].jankenHand == Player.HAND_GU && player[1].jankenHand == Player.HAND_CHOKI
            || player[0].jankenHand == Player.HAND_CHOKI && player[1].jankenHand == Player.HAND_PA
            || player[0].jankenHand == Player.HAND_PA && player[1].jankenHand == Player.HAND_GU)
        {
            Debug.Log(player[0].name + " の勝ち！！");

            player[0].isWinner = true;      // 勝利フラグを立てる

            // フェイズを次に進める
            phasePattern = PHASE_DAMAGE_CALC;
        }
        // 2Pが勝つパターン
        else if (player[1].jankenHand == Player.HAND_GU && player[0].jankenHand == Player.HAND_CHOKI
            || player[1].jankenHand == Player.HAND_CHOKI && player[0].jankenHand == Player.HAND_PA
            || player[1].jankenHand == Player.HAND_PA && player[0].jankenHand == Player.HAND_GU)
        {
            Debug.Log(player[1].name + " の勝ち！！");

            player[1].isWinner = true;      // 勝利フラグを立てる

            // フェイズを次に進める
            phasePattern = PHASE_DAMAGE_CALC;
        }
        else
        {
            Debug.Log("おあいこ...");

            // フェイズを手の選択に戻す
            phasePattern = PHASE_HAND_SELECT_P1;
        }
    }

    // === ダメージ計算
    public void DamageCalc()
    {
        Debug.Log("ダメージ計算を行います。");

        if (player[0].isWinner == true)
        {   // P1が勝ち、P2にダメージを与える
            player[1].hitPoint -= 1;
        }
        else if (player[1].isWinner == true)
        {   // P2が勝ち、P1にダメージを与える
            player[0].hitPoint -= 1;
        }

        // フェイズを次に進める
        phasePattern = PHASE_BATTLE_RESULT;
    }

    // === バトルリザルト
    public void Result()
    {
        if (player[0].isWinner == true)
        {   // P1の勝利表示
            Debug.Log(player[0].name + "さんの勝ちです。" + player[1].name + "さんに 1 のダメージ！");

            // プレイヤー1からSEを再生する
            player[0].GetComponent<AudioSource>().PlayOneShot(seArray[1]);
        }
        else if (player[1].isWinner == true)
        {   // P2の勝利表示
            Debug.Log(player[1].name + "さんの勝ちです。" + player[0].name + "さんに 1 のダメージ！");

            // プレイヤー2からSEを再生する
            player[1].GetComponent<AudioSource>().PlayOneShot(seArray[1]);
        }

        // フェイズを次に進める
        phasePattern = PHASE_ENTRY;
    }


    // ===
    // ログの表示（デバッグ用）
    // ===
    public void OnGUI()
    {
        GUILayout.Label("Turn >>> " + turnCount);

        // HP表示
        GUILayout.Label("Player1 HP >>> " + player[0].hitPoint);
        GUILayout.Label("Player2 HP >>> " + player[1].hitPoint);
    }
}
