using UnityEngine;

public class BossGenerator : MonoBehaviour
{
    [SerializeField] GameObject bossPrefab;
    [SerializeField] GameObject spawnPosition;

    private void OnTriggerEnter(Collider other)
    {
        //ƒvƒŒƒCƒ„[‚ÉÚG‚µ‚½‚çEnemy‚ğSpawn‚³‚¹‚é
        if (other.CompareTag("Player"))
        {
            Debug.Log("pop");
            Instantiate(bossPrefab, spawnPosition.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
