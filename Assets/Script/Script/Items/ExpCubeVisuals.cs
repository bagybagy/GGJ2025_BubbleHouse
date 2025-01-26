// ExpCubeVisuals.cs (�����ڂ̕���)
using UnityEngine;

public class ExpCubeVisuals : MonoBehaviour
{
    [SerializeField] float floatHeight = 0.5f; // ���V�̍����i�㉺�ɓ����͈́j
    [SerializeField] float floatSpeed = 1f;    // ���V�̑����iSin�g�̕ω����x�j
    [SerializeField] float floatOffsetY = 0.5f;    // ���V�̑����iSin�g�̕ω����x�j


    [SerializeField] float rotationSpeed = 10f; // ��]���x�iy���̉�]�ʁj

    void Update()
    {
        // === �㉺���V�̓��� ===
        // ���[�J����Ԋ�ŏ㉺���V������
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.localPosition = new Vector3(0, newY + floatOffsetY, 0); // ���[�J����Ԃŏ㉺��

        // === ��]�̓��� ===
        float newRot = Time.time * rotationSpeed; 

        // ��]�p�x��Quaternion.Euler�Őݒ�
        transform.rotation = Quaternion.Euler(newRot, 0, newRot);
    }
}
