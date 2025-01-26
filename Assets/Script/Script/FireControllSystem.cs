using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControllSystem : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 10f; // ターゲット検出範囲
    [SerializeField] private LayerMask targetLayer;       // ターゲットを特定するレイヤー
    [SerializeField] private GameObject laserPrefab;      // レーザーのPrefab
    [SerializeField] private Transform laserSpawnPoint;   // レーザーを発射する位置

    private List<Transform> targets = new List<Transform>(); // 検出された敵を格納するリスト

    [SerializeField] private GameObject magicPrefab;      // リニア弾のPrefab
    [SerializeField] private float delta = 0.5f;          // リニア弾発射位置の誤差範囲（上下左右）
    [SerializeField] private float shotInterval = 0.1f;   // リニア弾発射間隔（秒）
    [SerializeField] private int playerLv = 1;          // （Lv依存）
    [SerializeField] Camera playerCamera; // プレイヤーが使用するカメラ

    public void MultiMagicLaser()
    {
        GetLv();
        DetectTargets(); // ターゲットを検出する
        FireLasers();

    }

    public void LinearMagicShot()
    {
        GetLv();
        // Lv分の魔法弾を0.1秒間隔で発射
        StartCoroutine(SpawnMagicShots());

    }

    private IEnumerator SpawnMagicShots()
    {
        // カメラの中央から見ている方向を取得
        Vector3 shootDirection = playerCamera.transform.forward;

        for (int i = 0; i < playerLv; i++)
        {
            // 発射位置にランダムな誤差を追加（上下左右）
            Vector3 randomOffset = new Vector3(
                Random.Range(-delta, delta),  // X軸方向の誤差
                Random.Range(-delta, delta),  // Y軸方向の誤差
                Random.Range(-delta, delta)   // Z軸方向の誤差（必要であれば）
            );


            // 発射場所を決定
            Vector3 spawnPosition = laserSpawnPoint.position + randomOffset;

            // 魔法の弾を発射
            GameObject magicShot = Instantiate(magicPrefab, spawnPosition, transform.rotation);

            magicShot.transform.forward = shootDirection + (randomOffset * 0.1f);


            // 0.1秒待機して次の弾を発射
            yield return new WaitForSeconds(shotInterval);
        }
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
        int laserCount = 0;

        foreach (Transform target in targets)
        {
            if (laserCount > playerLv)    break;  // Lvによる発射数制限

            Debug.Log("LLL");

            // Playerの子オブジェクトとして生成（StatusManagerをPearentで参照するため）
            GameObject laser = Instantiate(laserPrefab, laserSpawnPoint.position, Quaternion.identity,transform);
            SimpleLaser simpleLaser = laser.GetComponent<SimpleLaser>();
            if (simpleLaser != null)
            {
                simpleLaser.SetTarget(target); // ターゲットを設定
            }

            laserCount++;
        }
    }

    // デバッグ用に範囲を可視化する
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void GetLv()
    {
        //プレイヤーレベルを取得、各種発射数に使いまわし
        playerLv = (int)GetComponent<StatusManager>().ExpRate;
    }
}
