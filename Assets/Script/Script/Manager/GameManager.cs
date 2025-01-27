using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    /// <summary>
    /// ボスが倒された際に呼び出される関数
    /// ゲームクリアの判定を行います
    /// </summary>
    public void NotifyBossDefeat()
    {
        // ゲームクリア時の処理を開始
        Debug.Log("Game Cleared!");  // ゲームクリアのログを表示

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
