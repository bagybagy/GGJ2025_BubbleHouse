using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    [SerializeField]
    float speed = 10f; // ”­ŽË‘¬“x
    [SerializeField]
    float endTime = 5f; // Á–ÅŽžŠÔ

    float deadTimer = 0f;


    private void Update()
    {
        // ‘O•û‚ÉŒü‚©‚Á‚ÄˆÚ“®
        transform.position += transform.forward * speed * Time.deltaTime;

        deadTimer = deadTimer + Time.deltaTime;
        if(deadTimer >= endTime)
        {
            Destroy(gameObject);
        }

    }
}