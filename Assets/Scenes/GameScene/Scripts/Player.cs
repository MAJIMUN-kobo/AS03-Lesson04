using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public const int HAND_GU = 0;       // �萔�F�O�[
    public const int HAND_CHOKI = 1;    // �萔�F�`���L
    public const int HAND_PA = 2;       // �萔�F�p�[

    public int jankenHand = 0;          // ����񂯂�ŏo������
    public bool isWinner = false;       // ���s����

    // �X�e�[�^�X
    public int hitPoint = 10;           // HP

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( hitPoint <= 0 )
        {   // HP�� 0 �ɂȂ����ꍇ
            hitPoint = 0;           // �}�C�i�X�ɂȂ�Ȃ��悤�ɒ���

            this.GetComponent<Rigidbody>().AddForce(this.transform.forward * -10, ForceMode.Impulse);
        }
    }
}
