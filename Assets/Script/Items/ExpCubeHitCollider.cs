using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCubeHitCollider : MonoBehaviour
{
    // ExpCube�ւ̎Q��
    private ExpCube expCube;
    void Start()
    {
        // �e�I�u�W�F�N�g�� StatusManager ���擾
        expCube = GetComponentInParent<ExpCube>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"ExpCube�R���C�_�[���m�I");
            // Player�ɐڐG������AExpCube�̏������Ăяo��
            expCube.GetExpCube(other); // Player��StatusManager��n��
        }
    }
}
