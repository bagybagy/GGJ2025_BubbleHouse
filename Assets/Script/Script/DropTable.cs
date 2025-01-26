using UnityEngine;

public class DropTable : MonoBehaviour
{
    [SerializeField] private GameObject[] itemsToDrop; // ドロップするアイテムの配列

    // アイテムドロップ処理
    public void DropItems()
    {
        foreach (GameObject item in itemsToDrop)
        {
            // アイテムを生成して地面に落とす
            Instantiate(item, transform.position, Quaternion.identity);
        }
    }
}
