using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum EnemyState
    {
        Idle,       // 立ち止まって何もしない
        Attack,     // プレイヤーを攻撃する
        Damaged     // 被弾中（ノックバック状態）
    }

    [SerializeField] private EnemyState currentState = EnemyState.Idle; // 現在の敵の状態
    [SerializeField] private float attackRange = 1.5f; // プレイヤーを攻撃できる範囲
    [SerializeField] private float knockbackDuration = 0.5f; // ノックバックが続く時間
    [SerializeField] private float knockbackForce = 5f; // ノックバックの強さ
    [SerializeField] private float idleDuration = 2f;    //アイドル状態の続く時間

    [SerializeField] private float idleTimer = 0f; // アイドル状態の経過時間

    private float damagedTimer = 0f; // ノックバック状態の経過時間
    private Vector3 knockbackDirection; // ノックバックの方向を記憶
    private Rigidbody rb; // Rigidbody参照
    private bool isKnockbackApplied = false; // ノックバックが適用されたかどうかを管理するフラグ

    private IEnemyAction enemyAction;

    void Start()
    {
        // Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
        // 攻撃用のクラスをインターフェイスで取得、これで複数種類の敵にも対応
        enemyAction = GetComponent<IEnemyAction>();

        if (enemyAction == null)
        {
            Debug.LogWarning("No attack algorithm attached to this enemy!");
        }

    }

    void Update()
    {
        // 敵の現在の状態に応じて処理を分岐します
        switch (currentState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Damaged:
                Damaged();
                break;
        }
    }

    private void Idle()
    {
        // 立ち止まっている時間をカウント
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
        {
            // StateをAttackに進行し、idleTimerを初期化
            currentState = EnemyState.Attack;
            idleTimer = 0f;
        }
    }

    private void Attack()
    {
        if (enemyAction != null)
        {
            enemyAction.Execute();
        }
        else
        {
            Debug.LogWarning("No attack behavior assigned!");
        }

    }

    private void Damaged()
    {
        // 被弾中の経過時間を加算
        damagedTimer += Time.deltaTime;

        if (!isKnockbackApplied)
        {
            // ノックバックを一度だけ適用
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            isKnockbackApplied = true; // ノックバックが適用されたことを記録
        }

        // ノックバックが終了したらIdle状態に戻る
        if (damagedTimer >= knockbackDuration)
        {
            currentState = EnemyState.Idle;
            isKnockbackApplied = false; // ノックバック適用フラグをリセット
        }
    }

    // 外部から呼び出されるダメージ処理
    public void TakeDamage(Vector3 attackSource)
    {
        // 被弾時にDamaged状態に遷移
        currentState = EnemyState.Damaged;

        // タイマーをリセット
        damagedTimer = 0f;
        idleTimer = 0f;

        // インターフェイス経由で攻撃停止を入れる
        if (enemyAction != null)
        {
            enemyAction.Cancel();
        }

        // 攻撃元から見たノックバックの方向を計算
        knockbackDirection = (transform.position - attackSource).normalized;
        knockbackDirection.y = 1f;      //ノックアップ用にy軸は１fを設定

        Debug.Log("Enemy is taking damage!");
    }
}
