using UnityEngine;

public class BossGenerator : MonoBehaviour
{
    [SerializeField] GameObject bossPrefab;
    [SerializeField] GameObject spawnPosition;

    private void OnTriggerEnter(Collider other)
    {
        //プレイヤーに接触したらEnemyをSpawnさせる
        if (other.CompareTag("Player"))
        {
            Debug.Log("pop");
            Instantiate(bossPrefab, spawnPosition.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
