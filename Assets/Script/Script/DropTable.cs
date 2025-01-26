using UnityEngine;

public class DropTable : MonoBehaviour
{
    [SerializeField] private GameObject[] itemsToDrop; // �h���b�v����A�C�e���̔z��

    // �A�C�e���h���b�v����
    public void DropItems()
    {
        foreach (GameObject item in itemsToDrop)
        {
            // �A�C�e���𐶐����Ēn�ʂɗ��Ƃ�
            Instantiate(item, transform.position, Quaternion.identity);
        }
    }
}
