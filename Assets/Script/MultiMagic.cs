using System.Collections.Generic;
using UnityEngine;

public class MultiMagic : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 10f; // �^�[�Q�b�g���o�͈�
    [SerializeField] private LayerMask targetLayer;       // �^�[�Q�b�g����肷�郌�C���[
    [SerializeField] private GameObject laserPrefab;      // ���[�U�[��Prefab
    [SerializeField] private Transform laserSpawnPoint;   // ���[�U�[�𔭎˂���ʒu

    private List<Transform> targets = new List<Transform>(); // ���o���ꂽ�G���i�[���郊�X�g

    public void MultiMagicLaser()
    {
        DetectTargets(); // �^�[�Q�b�g�����o����
        FireLasers();

    }

    // �͈͓��̃^�[�Q�b�g�����o����
    private void DetectTargets()
    {
        targets.Clear(); // �O�t���[���̃^�[�Q�b�g���X�g�����Z�b�g

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, targetLayer);

        foreach (var collider in hitColliders)
        {
            targets.Add(collider.transform);
        }
    }

    // ���[�U�[���^�[�Q�b�g�ɔ��˂���
    private void FireLasers()
    {
        foreach (Transform target in targets)
        {
            GameObject laser = Instantiate(laserPrefab, laserSpawnPoint.position, Quaternion.identity);
            SimpleLaser simpleLaser = laser.GetComponent<SimpleLaser>();
            if (simpleLaser != null)
            {
                simpleLaser.SetTarget(target); // �^�[�Q�b�g��ݒ�
            }
        }
    }

    // �f�o�b�O�p�ɔ͈͂���������
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
