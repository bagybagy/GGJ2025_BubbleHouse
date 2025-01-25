using UnityEngine;
using System.Collections;

public class EnemyActionshoot : MonoBehaviour, IEnemyAction
{
    [SerializeField] GameObject bulletPrefab; // 弾のPrefab（銃弾など）を指定
    [SerializeField] Transform firePoint; // 弾を発射する位置（銃口など）
    [SerializeField] float fireCooldown = 2f; // 弾を発射するクールダウン時間（次に発射するまでの待機時間）
    [SerializeField] Transform target; // プレイヤーの位置を指定（追尾対象）

    private bool isFiring = false; // 弾を発射しているかどうかを追跡するフラグ

    void Start()
    {
        // ゲーム開始時にプレイヤー（target）を見つけ、ターゲットを設定
        // プレイヤーがシーンにいる前提
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    public void Execute()
    {
        // 弾を発射する処理を開始
        // すでに発射していなければ、発射ルーチンを開始
        if (!isFiring)
        {
            StartCoroutine(FireRoutine());
        }
    }

    public void Cancel()
    {
        // 発射をキャンセルするためのメソッド
        // isFiringをfalseにして、発射を停止する
        isFiring = false;
    }

    private IEnumerator FireRoutine()
    {
        // 発射ルーチン
        isFiring = true; // 発射中のフラグを立てる
        while (isFiring)
        {
            // プレイヤーの方向を向く処理を実行
            RotateTowardsTarget();

            // 弾を発射（指定した位置でプレイヤーの方向を向いた弾を生成）
            Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            Debug.Log("Enemy is firing!"); // デバッグメッセージ（発射していることを確認）

            // クールダウン時間を待機
            // 発射後に指定した時間待ってから次の発射を行う
            yield return new WaitForSeconds(fireCooldown);
        }
    }

    // プレイヤーの方向を向く処理
    private void RotateTowardsTarget()
    {
        // ターゲット（プレイヤー）が存在する場合
        if (target != null)
        {
            // プレイヤーの位置を基準に方向を計算
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            directionToTarget.y = 0f; // 水平面だけで回転を制御（高さ方向は無視）

            // 方向がゼロでない場合（向くべき方向がある場合）
            if (directionToTarget != Vector3.zero)
            {
                // プレイヤー方向に向くための回転を計算
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                transform.rotation = targetRotation; // 即座にその方向を向く

                // 以下のコメントアウト部分を有効にすると、スムーズに回転するようになります
                // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed); // スムーズに向く場合
            }
        }
    }
}
