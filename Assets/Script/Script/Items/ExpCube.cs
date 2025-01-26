using UnityEngine;

public class ExpCube : MonoBehaviour
{
    [SerializeField]
    private float ExpIncreaseAmount = 0.1f; // �����ʁi�f�t�H���g��0.1�j

    private bool isCollected = false;

    [SerializeField] GameObject itemGetEffect;

    // Player�ɐڐG�����Ƃ��ɌĂяo����郁�\�b�h
    public void GetExpCube(Collider other)
    {
        if (isCollected) return;  // ���łɎ擾�ς݂Ȃ珈�����Ȃ�

        // ����Collider����StatusManager���擾�iPlayer��StatusManager�̂݁j
        StatusManager statusManager = other.GetComponentInParent<StatusManager>();

        if (statusManager != null)
        {
            isCollected = true;
            // IncreaseExp���Ăяo���A�����Ƃ��đ����ʂ�n��
            statusManager.IncreaseExp(ExpIncreaseAmount);

            // ���O (�f�o�b�O�p)
            Debug.Log($"ExpCube�擾�I exp���� {ExpIncreaseAmount} �������܂����B");

            // �A�C�e���擾���̃G�t�F�N�g
            var effect = Instantiate(itemGetEffect);
            effect.transform.position = transform.position;

            // ���̃A�C�e��������
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Player��StatusManager��������܂���B");
        }
    }
}
