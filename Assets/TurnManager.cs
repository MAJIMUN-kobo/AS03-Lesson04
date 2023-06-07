using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

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

    // === �G�t�F�N�g�n
    [Header("=== �G�t�F�N�g�ݒ� ===")]
    public GameObject[] effectArray = null;     // �G�t�F�N�g�̔z��
    public GameObject currentEffect = null;     // ���ݏo���Ă���G�t�F�N�g

    // === �T�E���h�n
    [Header("=== �T�E���h�ݒ� ===")]
    public AudioClip[] seArray = null;      // SE�̔z��
    public float seVolume = 1;              // SE�̉���
    public AudioClip[] bgmArray = null;     // BGM�̔z��
    public float bgmVolume = 1;             // BGM�̉���


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

        // P1��������
        player[0].isWinner = false;

        // P2��������
        player[1].isWinner = false;

        // �t�F�C�Y�����ɐi�߂�
        phasePattern = PHASE_HAND_SELECT_P1;

        // �v���C���[1����I�𒆂̃G�t�F�N�g���o��
        Vector3 effectPos = player[0].gameObject.transform.position;
        currentEffect = Instantiate(effectArray[0], effectPos, Quaternion.Euler(-90, 0, 0), player[0].transform);

        // BGM���Đ�
        this.GetComponent<AudioSource>().clip = bgmArray[0];
        //this.GetComponent<AudioSource>().loop = true;
        this.GetComponent<AudioSource>().Play();
    }

    // === P1�F��̑I��
    public void HandSelectP1()
    {
        Debug.Log("�v���C���[1 >> ���I��ł��������B");

        if (Input.GetKeyDown(KeyCode.Alpha1) == true)
        {   // "1�L�["�ŃO�[��I��
            player[0].jankenHand = Player.HAND_GU;

            Debug.Log("�v���C���[1 >> �O�[���o���܂����B");

            phasePattern = PHASE_HAND_SELECT_P2;    // �t�F�C�Y�����ɐi�߂�

            // �G�t�F�N�g���폜
            Destroy(currentEffect.gameObject);

            // �v���C���[2����G�t�F�N�g���o��
            Vector3 effectPos = player[1].transform.position;
            currentEffect = Instantiate(effectArray[0], effectPos, Quaternion.Euler(-90, 0, 0));

            // �v���C���[1����SE���Đ�����
            player[0].GetComponent<AudioSource>().PlayOneShot(seArray[0]);

            // BGM��ς���
            this.GetComponent<AudioSource>().clip = bgmArray[1];
            this.GetComponent<AudioSource>().Play();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) == true)
        {   // "2�L�["�Ń`���L��I��
            player[0].jankenHand = Player.HAND_CHOKI;

            Debug.Log("�v���C���[1 >> �`���L���o���܂����B");

            phasePattern = PHASE_HAND_SELECT_P2;    // �t�F�C�Y�����ɐi�߂�

            // �G�t�F�N�g���폜
            Destroy(currentEffect.gameObject);

            // �v���C���[2����G�t�F�N�g���o��
            Vector3 effectPos = player[1].transform.position;
            currentEffect = Instantiate(effectArray[0], effectPos, Quaternion.Euler(-90, 0, 0));

            // �v���C���[1����SE���Đ�����
            player[0].GetComponent<AudioSource>().PlayOneShot(seArray[0]);
        }

        if(Input.GetKeyDown(KeyCode.Alpha3) == true)
        {   // "3�L�["�Ńp�[��I��
            player[0].jankenHand = Player.HAND_PA;

            Debug.Log("�v���C���[1 >> �p�[���o���܂����B");

            phasePattern = PHASE_HAND_SELECT_P2;    // �t�F�C�Y�����ɐi�߂�

            // �G�t�F�N�g���폜
            Destroy(currentEffect.gameObject);

            // �v���C���[2����G�t�F�N�g���o��
            Vector3 effectPos = player[1].transform.position;
            currentEffect = Instantiate(effectArray[0], effectPos, Quaternion.Euler(-90, 0, 0));

            // �v���C���[1����SE���Đ�����
            player[0].GetComponent<AudioSource>().PlayOneShot(seArray[0]);
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

            // �G�t�F�N�g���폜
            Destroy(currentEffect.gameObject);

            // �v���C���[2����SE���Đ�����
            player[1].GetComponent<AudioSource>().PlayOneShot(seArray[0]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) == true)
        {   // "1�L�["�Ń`���L��I��
            player[1].jankenHand = Player.HAND_CHOKI;

            Debug.Log("�v���C���[2 >> �`���L���o���܂����B");

            phasePattern = PHASE_BATTLE;    // �t�F�C�Y�����ɐi�߂�

            // �G�t�F�N�g���폜
            Destroy(currentEffect.gameObject);

            // �v���C���[2����SE���Đ�����
            player[1].GetComponent<AudioSource>().PlayOneShot(seArray[0]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) == true)
        {   // "1�L�["�Ńp�[��I��
            player[1].jankenHand = Player.HAND_PA;

            Debug.Log("�v���C���[2 >> �p�[���o���܂����B");

            phasePattern = PHASE_BATTLE;    // �t�F�C�Y�����ɐi�߂�

            // �G�t�F�N�g���폜
            Destroy(currentEffect.gameObject);

            // �v���C���[2����SE���Đ�����
            player[1].GetComponent<AudioSource>().PlayOneShot(seArray[0]);
        }
    }

    // === �o�g������
    public void Battle()
    {
        Debug.Log("����񂯂�̔�����s���܂��B");

        // 1P�����p�^�[��
        if (player[0].jankenHand == Player.HAND_GU && player[1].jankenHand == Player.HAND_CHOKI
            || player[0].jankenHand == Player.HAND_CHOKI && player[1].jankenHand == Player.HAND_PA
            || player[0].jankenHand == Player.HAND_PA && player[1].jankenHand == Player.HAND_GU)
        {
            Debug.Log(player[0].name + " �̏����I�I");

            player[0].isWinner = true;      // �����t���O�𗧂Ă�

            // �t�F�C�Y�����ɐi�߂�
            phasePattern = PHASE_DAMAGE_CALC;
        }
        // 2P�����p�^�[��
        else if (player[1].jankenHand == Player.HAND_GU && player[0].jankenHand == Player.HAND_CHOKI
            || player[1].jankenHand == Player.HAND_CHOKI && player[0].jankenHand == Player.HAND_PA
            || player[1].jankenHand == Player.HAND_PA && player[0].jankenHand == Player.HAND_GU)
        {
            Debug.Log(player[1].name + " �̏����I�I");

            player[1].isWinner = true;      // �����t���O�𗧂Ă�

            // �t�F�C�Y�����ɐi�߂�
            phasePattern = PHASE_DAMAGE_CALC;
        }
        else
        {
            Debug.Log("��������...");

            // �t�F�C�Y����̑I���ɖ߂�
            phasePattern = PHASE_HAND_SELECT_P1;
        }
    }

    // === �_���[�W�v�Z
    public void DamageCalc()
    {
        Debug.Log("�_���[�W�v�Z���s���܂��B");

        if (player[0].isWinner == true)
        {   // P1�������AP2�Ƀ_���[�W��^����
            player[1].hitPoint -= 1;
        }
        else if (player[1].isWinner == true)
        {   // P2�������AP1�Ƀ_���[�W��^����
            player[0].hitPoint -= 1;
        }

        // �t�F�C�Y�����ɐi�߂�
        phasePattern = PHASE_BATTLE_RESULT;
    }

    // === �o�g�����U���g
    public void Result()
    {
        if (player[0].isWinner == true)
        {   // P1�̏����\��
            Debug.Log(player[0].name + "����̏����ł��B" + player[1].name + "����� 1 �̃_���[�W�I");

            // �v���C���[1����SE���Đ�����
            player[0].GetComponent<AudioSource>().PlayOneShot(seArray[1]);
        }
        else if (player[1].isWinner == true)
        {   // P2�̏����\��
            Debug.Log(player[1].name + "����̏����ł��B" + player[0].name + "����� 1 �̃_���[�W�I");

            // �v���C���[2����SE���Đ�����
            player[1].GetComponent<AudioSource>().PlayOneShot(seArray[1]);
        }

        // �t�F�C�Y�����ɐi�߂�
        phasePattern = PHASE_ENTRY;
    }


    // ===
    // ���O�̕\���i�f�o�b�O�p�j
    // ===
    public void OnGUI()
    {
        GUILayout.Label("Turn >>> " + turnCount);

        // HP�\��
        GUILayout.Label("Player1 HP >>> " + player[0].hitPoint);
        GUILayout.Label("Player2 HP >>> " + player[1].hitPoint);
    }
}
