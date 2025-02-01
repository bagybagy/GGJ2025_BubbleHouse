using System.Collections;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    private int currentBubbleCount = 0; // 現在のBubble数
    [SerializeField] private bool isExplorationPhase = false; // 探索フェーズの制御フラグ

    // 他所からの参照用
    public int CurrentBubbleCount
    {
        get { return currentBubbleCount; } 
    }
    public bool IsExplorationPhase
    {
        get { return isExplorationPhase; }
    }

    // シングルトンインスタンス
    public static BubbleManager Instance { get; private set; }

    private void Awake()
    {
        // シングルトンのセットアップ
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("BubbleManager は複数存在することはできません！");
            Destroy(gameObject); // 重複したインスタンスを削除
        }
    }

    private void Start()
    {
        // Bubbleの数を定期的にチェックするコルーチンを開始
        StartCoroutine(CheckBubbleCountRoutine());
    }

    // Bubbleの数を定期的に確認するコルーチン
    private IEnumerator CheckBubbleCountRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // 1秒ごとに確認（必要に応じて調整）

            // デバッグ用ログ
            Debug.Log("Current Bubble Count: " + currentBubbleCount); // 現在のBubble数をログに出力

            // Bubbleタグを持つオブジェクトの数を取得(.Lengthで数が取得できる)
            currentBubbleCount = GameObject.FindGameObjectsWithTag("Bubble").Length;
            if (currentBubbleCount <= 0 && !isExplorationPhase)
            {
                isExplorationPhase = true;  // 探索フェーズに切り替え

                Debug.Log("Bubble Clear");   //Bubbleがすべて倒された判定、演出やフェーズ変更等を入れてもよ

                // Bubbleが再度出現するまで待機
                while (currentBubbleCount <= 0)
                {
                    currentBubbleCount = GameObject.FindGameObjectsWithTag("Bubble").Length; // Bubbleの数を再確認
                    yield return new WaitForSeconds(1f); // 1秒ごとに再確認
                }

                isExplorationPhase = false; // 再度Bubbleが出現したら探索フェーズを終了
            }
        }
    }
}