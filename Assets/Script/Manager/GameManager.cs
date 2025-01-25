using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private enum GameState
    {
        Intro,      // 立ち止まって何もしない
        Phase1,     // 最初の部屋
        Phase1_Outro,// 扉が開く処理
        Phase2,     // 階段踊り場
        Phase2_Outro,// 寝室への扉が開く処理
        Phase3,     // 寝室ボス戦
        Phase3_Outro,// エンディングへ
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

    // シングルトンインスタンス
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // シングルトンのセットアップ
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("GameManager は複数存在することはできません！");
            Destroy(gameObject); // 重複したインスタンスを削除
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 敵の現在の状態に応じて処理を分岐します
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
        // イントロ処理があればここに記載
        currentGameState = GameState.Phase1;
    }

    private void Phase1()
    {
        if(BubbleManager.Instance.IsExplorationPhase == false)
        {
            return;
        }
        // 処理があればここに記載
        currentGameState = GameState.Phase1_Outro;
    }
    private void Phase1_Outro()
    {
        phase2Bubble.SetActive(true);

        if (BubbleManager.Instance.IsExplorationPhase == true)
        {
            return;
        }
        // 処理があればここに記載
        DoorOpen door = phase1_Door.GetComponent<DoorOpen>();
        if (door != null)
        {
            door.Open();
        }
        else
        {
            Debug.LogError("Phase1_Door に LivingDoorOpen コンポーネントが見つかりません！");
        }

        currentGameState = GameState.Phase2;
    }
    private void Phase2()
    {
        if (BubbleManager.Instance.IsExplorationPhase == false)
        {
            return;
        }
        // 処理があればここに記載
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
            Debug.LogError("Door に LivingDoorOpen コンポーネントが見つかりません！");
        }
        // 処理があればここに記載
        currentGameState = GameState.Phase3;
    }
    private void Phase3()
    {
        if (BubbleManager.Instance.IsExplorationPhase == false)
        {
            return;
        }
        // 処理があればここに記載
        currentGameState = GameState.Phase3_Outro;
    }
    private void Phase3_Outro()
    {
        phase4Bubble.SetActive(true);

        if (BubbleManager.Instance.IsExplorationPhase == true)
        {
            return;
        }
        // 処理があればここに記載
        DoorOpen doorC = phase3_Door_C.GetComponent<DoorOpen>();
        DoorOpen doorD = phase3_Door_D.GetComponent<DoorOpen>();

        if (doorC != null && doorD != null)
        {
            doorC.Open();
            doorD.Open();

        }
        else
        {
            Debug.LogError("Door に LivingDoorOpen コンポーネントが見つかりません！");
        }
        currentGameState = GameState.Phase4;

    }
    private void Phase4()
    {
        // 処理があればここに記載

    }

}
