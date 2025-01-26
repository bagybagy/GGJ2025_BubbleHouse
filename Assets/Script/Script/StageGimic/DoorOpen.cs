using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private float rotationDuration = 2f; // ��]�ɂ����鎞�ԁi�b�j

    private IEnumerator RotateOverTime(Quaternion rotationOffset)
    {
        Quaternion startRotation = transform.rotation; // ���݂̉�]
        Quaternion targetRotation = startRotation * rotationOffset; // �ڕW��]

        float elapsedTime = 0f;

        // �w�莞�Ԃ����ĉ�]
        while (elapsedTime < rotationDuration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // ���̃t���[���܂őҋ@
        }

        // ��]�𐳊m�ɖڕW�ɐݒ�
        transform.rotation = targetRotation;
    }
    public void Open()
    {
        // �R���[�`�����J�n����90�x��]
        StartCoroutine(RotateOverTime(Quaternion.Euler(0, 90, 0)));
    }

}
