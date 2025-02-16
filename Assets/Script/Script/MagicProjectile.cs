using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    [SerializeField]
    float speed = 10f; // 発射速度
    [SerializeField]
    float endTime = 5f; // 消滅時間

    float deadTimer = 0f;


    private void Update()
    {
        // 前方に向かって移動
        transform.position += transform.forward * speed * Time.deltaTime;

        deadTimer = deadTimer + Time.deltaTime;
        if(deadTimer >= endTime)
        {
            Destroy(gameObject);
        }

    }
}