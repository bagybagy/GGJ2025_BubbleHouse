using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private enum GameState
    {
        Intro,      // �����~�܂��ĉ������Ȃ�
        Phase1,     // �ŏ��̕���
        Phase1_Outro,// �����J������
        Phase2,     // �K�i�x���
        Phase2_Outro,// �Q���ւ̔����J������
        Phase3,     // �Q���{�X��
        Phase3_Outro,// �G���f�B���O��
        Phase4,
    }

    [SerializeField] GameState currentGameState = GameState.Intro;

    [SerializeField] GameObject phase1_Door;
    [SerializeField] GameObject phase2_Door_A;
    [SerializeField] GameObject phase2_Door_B;
    [SerializeField] GameObject phase3_Door_C;
    [SerializeField] GameObject phase3_Door_D;

    [SerializeField] GameObject phase2Bubble;
    [SerializeField] GameObject phase3Bubble;
    [SerializeField] GameObject phase4Bubble;

    // �V���O���g���C���X�^���X
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // �V���O���g���̃Z�b�g�A�b�v
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("GameManager �͕������݂��邱�Ƃ͂ł��܂���I");
            Destroy(gameObject); // �d�������C���X�^���X���폜
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �G�̌��݂̏�Ԃɉ����ď����𕪊򂵂܂�
        switch (currentGameState)
        {
            case GameState.Intro:
                Intro();
                break;
            case GameState.Phase1:
                Phase1();
                break;
            case GameState.Phase1_Outro:
                Phase1_Outro();
                break;
            case GameState.Phase2:
                Phase2();
                break;
            case GameState.Phase2_Outro:
                Phase2_Outro();
                break;
            case GameState.Phase3:
                Phase3();
                break;
            case GameState.Phase3_Outro:
                Phase3_Outro();
                break;
            case GameState.Phase4:
                Phase4();
                break;
        }
    }

    private void Intro()
    {
        // �C���g������������΂����ɋL��
        currentGameState = GameState.Phase1;
    }

    private void Phase1()
    {
        if(BubbleManager.Instance.IsExplorationPhase == false)
        {
            return;
        }
        // ����������΂����ɋL��
        currentGameState = GameState.Phase1_Outro;
    }
    private void Phase1_Outro()
    {
        phase2Bubble.SetActive(true);

        if (BubbleManager.Instance.IsExplorationPhase == true)
        {
            return;
        }
        // ����������΂����ɋL��
        DoorOpen door = phase1_Door.GetComponent<DoorOpen>();
        if (door != null)
        {
            door.Open();
        }
        else
        {
            Debug.LogError("Phase1_Door �� LivingDoorOpen �R���|�[�l���g��������܂���I");
        }

        currentGameState = GameState.Phase2;
    }
    private void Phase2()
    {
        if (BubbleManager.Instance.IsExplorationPhase == false)
        {
            return;
        }
        // ����������΂����ɋL��
        currentGameState = GameState.Phase2_Outro;
    }
    private void Phase2_Outro()
    {
        phase3Bubble.SetActive(true);

        if (BubbleManager.Instance.IsExplorationPhase == true)
        {
            return;
        }

        DoorOpen doorA = phase2_Door_A.GetComponent<DoorOpen>();
        DoorOpen doorB = phase2_Door_B.GetComponent<DoorOpen>();

        if (doorA != null && doorB != null)
        {
            doorA.Open();
            doorB.Open();

        }
        else
        {
            Debug.LogError("Door �� LivingDoorOpen �R���|�[�l���g��������܂���I");
        }
        // ����������΂����ɋL��
        currentGameState = GameState.Phase3;
    }
    private void Phase3()
    {
        if (BubbleManager.Instance.IsExplorationPhase == false)
        {
            return;
        }
        // ����������΂����ɋL��
        currentGameState = GameState.Phase3_Outro;
    }
    private void Phase3_Outro()
    {
        phase4Bubble.SetActive(true);

        if (BubbleManager.Instance.IsExplorationPhase == true)
        {
            return;
        }
        // ����������΂����ɋL��
        DoorOpen doorC = phase3_Door_C.GetComponent<DoorOpen>();
        DoorOpen doorD = phase3_Door_D.GetComponent<DoorOpen>();

        if (doorC != null && doorD != null)
        {
            doorC.Open();
            doorD.Open();

        }
        else
        {
            Debug.LogError("Door �� LivingDoorOpen �R���|�[�l���g��������܂���I");
        }
        currentGameState = GameState.Phase4;

    }
    private void Phase4()
    {
        // ����������΂����ɋL��

    }

}
