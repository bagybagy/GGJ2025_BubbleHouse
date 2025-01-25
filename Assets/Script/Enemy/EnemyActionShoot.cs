using UnityEngine;
using System.Collections;

public class EnemyActionshoot : MonoBehaviour, IEnemyAction
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireCooldown = 2f;
    [SerializeField] Transform target;              // Playerの位置を指定

    private bool isFiring = false;

    void Start()
    {
        // 追尾対象の取得（Playerを想定）
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    public void Execute()
    {
        if (!isFiring)
        {
            StartCoroutine(FireRoutine());
        }
    }

    public void Cancel()
    {
        isFiring = false;
    }

    private IEnumerator FireRoutine()
    {
        isFiring = true;
        while (isFiring)
        {
            // プレイヤーの方向を向く
            RotateTowardsTarget();

            Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            Debug.Log("Enemy is firing!");
            yield return new WaitForSeconds(fireCooldown);
        }
    }
    // Playerの方向を向く処理
    private void RotateTowardsTarget()
    {
        if (target != null)
        {
            // プレイヤーの方向を計算
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            directionToTarget.y = 0f; // 水平面だけで回転を制御

            // 対象方向を向く
            if (directionToTarget != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                transform.rotation = targetRotation; // 即座に向く場合
                                                     // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed); // スムーズに向く場合
            }
        }
    }

}
