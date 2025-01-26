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

    // ゲームクリアの状態を保持する変数
    // クリアしたかどうかのフラグです
    private bool isGameCleared = false;

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
        // カメラをフェイズ2ドア用に一瞬変更、引数はフェイズ番号
        CameraManager.Instance.SwitchToPhaseCamera(2);

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
        // カメラをフェイズ2ドア用に一瞬変更、引数はフェイズ番号
        CameraManager.Instance.SwitchToPhaseCamera(3);
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
        // カメラをフェイズ2ドア用に一瞬変更、引数はフェイズ番号
        CameraManager.Instance.SwitchToPhaseCamera(4);
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

    /// <summary>
    /// ボスが倒された際に呼び出される関数
    /// ゲームクリアの判定を行います
    /// </summary>
    public void NotifyBossDefeat()
    {
        // ゲームがすでにクリアされている場合、もう一度クリア判定を行わないようにします
        if (isGameCleared)
        {
            return; // すでにゲームクリアになっている場合は、処理をスキップ
        }

        // ゲームクリア時の処理を開始
        Debug.Log("Game Cleared!");  // ゲームクリアのログを表示

        // ゲームクリア状態をtrueにして、二度とクリア処理を行わないようにする
        isGameCleared = true;

        // ゲームクリア時の追加処理（例: UI更新やシーン遷移など）
        HandleGameClear();
    }

    /// <summary>
    /// ゲームクリア時に実行する具体的な処理をまとめたメソッド
    /// </summary>
    private void HandleGameClear()
    {
        // ゲームクリア時に行いたい処理をここに書きます
        Debug.Log("Congratulations! You have cleared the game!");
        // クリアUIの呼び出し
        UIManager.Instance.ActiveClearUI();

        // 例: ゲームクリアのUIを表示したり、シーンを遷移させたりする処理
        // SceneManager.LoadScene("GameClearScene"); // 例えば、ゲームクリア後に別のシーンに遷移する場合
    }

}
