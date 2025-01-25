using System.Collections.Generic;
using UnityEngine;

public class MultiMagic : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 10f; // ターゲット検出範囲
    [SerializeField] private LayerMask targetLayer;       // ターゲットを特定するレイヤー
    [SerializeField] private GameObject laserPrefab;      // レーザーのPrefab
    [SerializeField] private Transform laserSpawnPoint;   // レーザーを発射する位置

    private List<Transform> targets = new List<Transform>(); // 検出された敵を格納するリスト

    public void MultiMagicLaser()
    {
        DetectTargets(); // ターゲットを検出する
        FireLasers();

    }

    // 範囲内のターゲットを検出する
    private void DetectTargets()
    {
        targets.Clear(); // 前フレームのターゲットリストをリセット

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, targetLayer);

        foreach (var collider in hitColliders)
        {
            targets.Add(collider.transform);
        }
    }

    // レーザーをターゲットに発射する
    private void FireLasers()
    {
        foreach (Transform target in targets)
        {
            GameObject laser = Instantiate(laserPrefab, laserSpawnPoint.position, Quaternion.identity);
            SimpleLaser simpleLaser = laser.GetComponent<SimpleLaser>();
            if (simpleLaser != null)
            {
                simpleLaser.SetTarget(target); // ターゲットを設定
            }
        }
    }

    // デバッグ用に範囲を可視化する
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
