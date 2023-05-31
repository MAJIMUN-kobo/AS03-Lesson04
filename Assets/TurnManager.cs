using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    // �^�[�����̃t�F�C�Y >>>>
    public const int PHASE_HAND_SELECT_P1 = 0;  // �t�F�C�Y�F��̑I��(1P)
    public const int PHASE_HAND_SELECT_P2 = 1;  // �t�F�C�Y�F��̑I��(2P)
    public const int PHASE_BATTLE         = 2;  // �t�F�C�Y�F����񂯂񏈗�
    public const int PHASE_DAMAGE_CALC    = 3;  // �t�F�C�Y�F�_���[�W�v�Z
    public const int PHASE_BATTLE_RESULT  = 4;  // �t�F�C�Y�F���ʕ\��
    public const int PHASE_ENTRY          = 5;  // �t�F�C�Y�F�^�[���J�n���� 
    //  <<< ���s�����܂�܂ŏ�L���J��Ԃ�

    public int turnCount = 0;           // �^�[����
    public int phasePattern = 0;        // �t�F�C�Y���񂷂��߂̕ϐ�

    // === �v���C���[
    public Player[] player = null;


    // Start is called before the first frame update
    void Start()
    {
        phasePattern = PHASE_ENTRY;

        // === �v���C���[����������
        for(int i = 0; i < player.Length; i++) 
        {
            player[i].jankenHand = Player.HAND_GU;  // ��̏�����
        }
    }

    // Update is called once per frame
    void Update()
    {
        // === 
        /* >>> Swtch���̏����� <<<
           swtch(����)
           {
              case �p�^�[��:
                 >>> ����
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


    // === �^�[���̏�����
    public void TurnEntry()
    {
        Debug.Log("�^�[�����J�n���܂��B");

        turnCount++; // �^�[�����𑝂₵�܂��B

        // �t�F�C�Y�����ɐi�߂�
        phasePattern = PHASE_HAND_SELECT_P1;
    }

    // === P1�F��̑I��
    public void HandSelectP1()
    {
        Debug.Log("�v���C���[1 >> ���I��ł��������B");

        if(Input.GetKeyDown(KeyCode.Alpha1) == true)
        {   // "1�L�["�ŃO�[��I��
            player[0].jankenHand = Player.HAND_GU;

            Debug.Log("�v���C���[1 >> �O�[���o���܂����B");

            phasePattern = PHASE_HAND_SELECT_P2;    // �t�F�C�Y�����ɐi�߂�
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) == true)
        {   // "2�L�["�Ń`���L��I��
            player[0].jankenHand = Player.HAND_CHOKI;

            Debug.Log("�v���C���[1 >> �`���L���o���܂����B");

            phasePattern = PHASE_HAND_SELECT_P2;    // �t�F�C�Y�����ɐi�߂�
        }

        if(Input.GetKeyDown(KeyCode.Alpha3) == true)
        {   // "3�L�["�Ńp�[��I��
            player[0].jankenHand = Player.HAND_PA;

            Debug.Log("�v���C���[1 >> �p�[���o���܂����B");

            phasePattern = PHASE_HAND_SELECT_P2;    // �t�F�C�Y�����ɐi�߂�
        }
    }

    // === P2�F��̑I��
    public void HandSelectP2() 
    {
        Debug.Log("�v���C���[2 >> ���I��ł��������B");

        if (Input.GetKeyDown(KeyCode.Alpha1) == true)
        {   // "1�L�["�ŃO�[��I��
            player[1].jankenHand = Player.HAND_GU;

            Debug.Log("�v���C���[2 >> �O�[���o���܂����B");

            phasePattern = PHASE_BATTLE;    // �t�F�C�Y�����ɐi�߂�
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) == true)
        {   // "1�L�["�Ń`���L��I��
            player[1].jankenHand = Player.HAND_CHOKI;

            Debug.Log("�v���C���[2 >> �`���L���o���܂����B");

            phasePattern = PHASE_BATTLE;    // �t�F�C�Y�����ɐi�߂�
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) == true)
        {   // "1�L�["�Ńp�[��I��
            player[1].jankenHand = Player.HAND_PA;

            Debug.Log("�v���C���[2 >> �p�[���o���܂����B");

            phasePattern = PHASE_BATTLE;    // �t�F�C�Y�����ɐi�߂�
        }
    }

    // === �o�g������
    public void Battle()
    {
        Debug.Log("����񂯂�̔�����s���܂��B");

        // �t�F�C�Y�����ɐi�߂�
        phasePattern = PHASE_DAMAGE_CALC;
    }

    // === �_���[�W�v�Z
    public void DamageCalc()
    {
        Debug.Log("�_���[�W�v�Z���s���܂��B");

        // �t�F�C�Y�����ɐi�߂�
        phasePattern = PHASE_BATTLE_RESULT;
    }

    // === �o�g�����U���g
    public void Result()
    {
        Debug.Log("����������̏����ł��B�[�[�[����Ɂ@???�@�̃_���[�W�I");

        // �t�F�C�Y�����ɐi�߂�
        phasePattern = PHASE_ENTRY;
    }


    // ===
    // ���O�̕\���i�f�o�b�O�p�j
    // ===
    public void OnGUI()
    {
        GUILayout.Label("Turn >>> " + turnCount);
    }
}
