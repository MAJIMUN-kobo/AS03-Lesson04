using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public const int HAND_GU = 0;       // 定数：グー
    public const int HAND_CHOKI = 1;    // 定数：チョキ
    public const int HAND_PA = 2;       // 定数：パー

    public int jankenHand = 0;          // じゃんけんで出した手
    public bool isWinner = false;       // 勝敗判定

    // ステータス
    public int hitPoint = 10;           // HP

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( hitPoint <= 0 )
        {   // HPが 0 になった場合
            hitPoint = 0;           // マイナスにならないように調整

            this.GetComponent<Rigidbody>().AddForce(this.transform.forward * -10, ForceMode.Impulse);
        }
    }
}
