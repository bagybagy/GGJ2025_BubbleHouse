using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    [SerializeField] private float speed = 2f;                   // ボイドの速度
    [SerializeField] private float rotationSpeed = 1f;           // 回転の速さ
    [SerializeField] private float neighborRadius = 3f;          // 近くのボイドの検出範囲
    [SerializeField] private float separationRadius = 1f;        // 分離のための近距離範囲
    [SerializeField] private float alignmentStrength = 1f;       // 整列の強さ
    [SerializeField] private float cohesionStrength = 0.5f;      // 凝集の強さ
    [SerializeField] private Vector3 roomSize = new Vector3(10f, 10f, 10f); // 部屋のサイズ（ローカル座標系）

    private Vector3 velocity;                                      // ボイドの速度ベクトル
    private List<Boid> neighbors = new List<Boid>();               // 近くのボイド

    private Transform roomCenter;  // 部屋の中心位置（親オブジェクトを想定）

    void Start()
    {
        roomCenter = transform.parent;  // 部屋の親オブジェクトを取得（部屋の中心が親オブジェクトに設定されている前提）

        velocity = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * speed;
    }

    void Update()
    {
        FindNeighbors();

        // 各力を計算（分離、整列、凝集）
        Vector3 separation = Separation() * separationRadius;
        Vector3 alignment = Alignment() * alignmentStrength;
        Vector3 cohesion = Cohesion() * cohesionStrength;

        // 各力を制限する（最大速度を超えないように）
        separation = Vector3.ClampMagnitude(separation, speed);
        alignment = Vector3.ClampMagnitude(alignment, speed);
        cohesion = Vector3.ClampMagnitude(cohesion, speed);

        // それぞれの力を加算し、移動ベクトルを計算
        velocity = Vector3.Lerp(velocity, velocity + separation + alignment + cohesion, 0.1f);

        // 速度を更新（最大速度を維持）
        velocity = Vector3.ClampMagnitude(velocity, speed);

        // 移動
        transform.position += velocity * Time.deltaTime;

        // 回転をスムーズにする
        if (velocity.magnitude > 0.1f)  // 速度がほとんどゼロでない場合に回転
        {
            Quaternion targetRotation = Quaternion.LookRotation(velocity);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // 部屋の範囲をローカル座標系で制限
        LimitPositionWithinRoom();
    }


    // 近くのボイドを検出
    void FindNeighbors()
    {
        neighbors.Clear();

        // 近くのボイドを探す
        Boid[] allBoids = FindObjectsOfType<Boid>();
        foreach (Boid boid in allBoids)
        {
            if (boid != this)
            {
                float distance = Vector3.Distance(transform.position, boid.transform.position);
                if (distance < neighborRadius)
                {
                    neighbors.Add(boid);
                }
            }
        }
    }

    // 分離：他のボイドと衝突しないように
    Vector3 Separation()
    {
        Vector3 move = Vector3.zero;
        foreach (Boid boid in neighbors)
        {
            float distance = Vector3.Distance(transform.position, boid.transform.position);
            if (distance < separationRadius)
            {
                move += transform.position - boid.transform.position;
            }
        }
        return move;
    }

    // 整列：近くのボイドの方向に合わせる
    Vector3 Alignment()
    {
        Vector3 averageVelocity = Vector3.zero;
        foreach (Boid boid in neighbors)
        {
            averageVelocity += boid.velocity;
        }
        if (neighbors.Count > 0)
        {
            averageVelocity /= neighbors.Count;
            return averageVelocity - velocity;
        }
        return Vector3.zero;
    }

    // 凝集：周囲のボイドに向かって進む
    Vector3 Cohesion()
    {
        Vector3 centerOfMass = Vector3.zero;
        foreach (Boid boid in neighbors)
        {
            centerOfMass += boid.transform.position;
        }
        if (neighbors.Count > 0)
        {
            centerOfMass /= neighbors.Count;
            return (centerOfMass - transform.position).normalized * speed;
        }
        return Vector3.zero;
    }

    // 部屋のローカル範囲を制限する
    void LimitPositionWithinRoom()
    {
        // ボイドの位置をローカル座標に変換
        Vector3 localPosition = roomCenter.InverseTransformPoint(transform.position);

        // 部屋のサイズを基準に、ボイドの位置を制限
        localPosition.x = Mathf.Clamp(localPosition.x, -roomSize.x / 2, roomSize.x / 2);
        localPosition.y = Mathf.Clamp(localPosition.y, -roomSize.y / 2, roomSize.y / 2);
        localPosition.z = Mathf.Clamp(localPosition.z, -roomSize.z / 2, roomSize.z / 2);

        // ローカル座標からワールド座標に戻して位置を更新
        transform.position = roomCenter.TransformPoint(localPosition);
    }
}
