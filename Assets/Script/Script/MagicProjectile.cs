using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    [SerializeField]
    float speed = 10f; // ���ˑ��x
    [SerializeField]
    float endTime = 5f; // ���Ŏ���

    float deadTimer = 0f;


    private void Update()
    {
        // �O���Ɍ������Ĉړ�
        transform.position += transform.forward * speed * Time.deltaTime;

        deadTimer = deadTimer + Time.deltaTime;
        if(deadTimer >= endTime)
        {
            Destroy(gameObject);
        }

    }
}