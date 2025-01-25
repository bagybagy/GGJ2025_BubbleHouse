using UnityEngine;

public class ExpCube : MonoBehaviour
{
    [SerializeField]
    private float ExpIncreaseAmount = 0.1f; // 増加量（デフォルトは0.1）

    private bool isCollected = false;

    [SerializeField] GameObject itemGetEffect;

    // Playerに接触したときに呼び出されるメソッド
    public void GetExpCube(Collider other)
    {
        if (isCollected) return;  // すでに取得済みなら処理しない

        // 他のColliderからStatusManagerを取得（PlayerのStatusManagerのみ）
        StatusManager statusManager = other.GetComponentInParent<StatusManager>();

        if (statusManager != null)
        {
            isCollected = true;
            // IncreaseExpを呼び出し、引数として増加量を渡す
            statusManager.IncreaseExp(ExpIncreaseAmount);

            // ログ (デバッグ用)
            Debug.Log($"ExpCube取得！ exp率が {ExpIncreaseAmount} 増加しました。");

            // アイテム取得時のエフェクト
            var effect = Instantiate(itemGetEffect);
            effect.transform.position = transform.position;

            // このアイテムを消去
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("PlayerにStatusManagerが見つかりません。");
        }
    }
}
