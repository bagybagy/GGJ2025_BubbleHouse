using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] string tagName;            //�����蔻��ƂȂ�^�O
    private StatusManager statusManager;
    private Rigidbody rb;

    private Vector3 knockbackDirection; // �m�b�N�o�b�N�̕������L��
    [SerializeField] private float knockbackForce = 5f; // �m�b�N�o�b�N�̋���

    void Start()
    {
        // �e�I�u�W�F�N�g�� StatusManager��Rigidbody ���擾
        statusManager = GetComponentInParent<StatusManager>();
        rb = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // �w�肳�ꂽ�^�O�ƈ�v����I�u�W�F�N�g�ƐڐG�����ꍇ
        if (other.CompareTag(tagName))
        {
            Debug.Log("Hit registered by DamageTrigger");

            // �U���I�u�W�F�N�g�̐e�I�u�W�F�N�g���� StatusManager ���擾
            StatusManager attackerStatus = other.GetComponentInParent<StatusManager>();
            // �U���I�u�W�F�N�g�̈ʒu���擾�i�m�b�N�o�b�N�p�j
            Vector3 attackPoint = other.ClosestPoint(transform.position);


            int damageAmount = 1; // �f�t�H���g�̃_���[�W��
            float critAmount = 0f; // �f�t�H���g�̃_���[�W��

            // �e�� StatusManager �Ƀ_���[�W��ʒm
            if (statusManager != null)
            {
                if (attackerStatus != null)
                {
                    // �U�����̃_���[�W�ʂ��擾
                    damageAmount = attackerStatus.GetDamageAmount();
                }

                // �_���[�W�ʂ������֐��ɑ���
                statusManager.Damage(damageAmount, critAmount, attackPoint);

                // �m�b�N�o�b�N����
                ApplyKnockback(attackPoint);
            }
        }
    }

    private void ApplyKnockback(Vector3 attackPoint)
    {
        // �e�I�u�W�F�N�g��EnemyController�������Ă�����ATakeDamage�֐����Ăяo��
        //EnemyController enemyController = GetComponentInParent<EnemyController>();
        //if (enemyController != null)
        //{
        //    enemyController.TakeDamage(attackPoint);
        //}
    }
}
