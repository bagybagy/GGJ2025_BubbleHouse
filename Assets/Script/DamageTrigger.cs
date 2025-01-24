using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] string tagName;            //当たり判定となるタグ
    private StatusManager statusManager;
    private Rigidbody rb;

    private Vector3 knockbackDirection; // ノックバックの方向を記憶
    [SerializeField] private float knockbackForce = 5f; // ノックバックの強さ

    void Start()
    {
        // 親オブジェクトの StatusManagerとRigidbody を取得
        statusManager = GetComponentInParent<StatusManager>();
        rb = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 指定されたタグと一致するオブジェクトと接触した場合
        if (other.CompareTag(tagName))
        {
            Debug.Log("Hit registered by DamageTrigger");

            // 攻撃オブジェクトの親オブジェクトから StatusManager を取得
            StatusManager attackerStatus = other.GetComponentInParent<StatusManager>();
            // 攻撃オブジェクトの位置を取得（ノックバック用）
            Vector3 attackPoint = other.ClosestPoint(transform.position);


            int damageAmount = 1; // デフォルトのダメージ量
            float critAmount = 0f; // デフォルトのダメージ量

            // 親の StatusManager にダメージを通知
            if (statusManager != null)
            {
                if (attackerStatus != null)
                {
                    // 攻撃側のダメージ量を取得
                    damageAmount = attackerStatus.GetDamageAmount();
                }

                // ダメージ量を処理関数に送る
                statusManager.Damage(damageAmount, critAmount, attackPoint);

                // ノックバック処理
                ApplyKnockback(attackPoint);
            }
        }
    }

    private void ApplyKnockback(Vector3 attackPoint)
    {
        // 親オブジェクトがEnemyControllerを持っていたら、TakeDamage関数を呼び出し
        //EnemyController enemyController = GetComponentInParent<EnemyController>();
        //if (enemyController != null)
        //{
        //    enemyController.TakeDamage(attackPoint);
        //}
    }
}
