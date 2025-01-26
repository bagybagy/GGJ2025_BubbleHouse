using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControllSystem : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 10f; // �^�[�Q�b�g���o�͈�
    [SerializeField] private LayerMask targetLayer;       // �^�[�Q�b�g����肷�郌�C���[
    [SerializeField] private GameObject laserPrefab;      // ���[�U�[��Prefab
    [SerializeField] private Transform laserSpawnPoint;   // ���[�U�[�𔭎˂���ʒu

    private List<Transform> targets = new List<Transform>(); // ���o���ꂽ�G���i�[���郊�X�g

    [SerializeField] private GameObject magicPrefab;      // ���j�A�e��Prefab
    [SerializeField] private float delta = 0.5f;          // ���j�A�e���ˈʒu�̌덷�͈́i�㉺���E�j
    [SerializeField] private float shotInterval = 0.1f;   // ���j�A�e���ˊԊu�i�b�j
    [SerializeField] private int playerLv = 1;          // �iLv�ˑ��j
    [SerializeField] Camera playerCamera; // �v���C���[���g�p����J����

    public void MultiMagicLaser()
    {
        GetLv();
        DetectTargets(); // �^�[�Q�b�g�����o����
        FireLasers();

    }

    public void LinearMagicShot()
    {
        GetLv();
        // Lv���̖��@�e��0.1�b�Ԋu�Ŕ���
        StartCoroutine(SpawnMagicShots());

    }

    private IEnumerator SpawnMagicShots()
    {
        // �J�����̒������猩�Ă���������擾
        Vector3 shootDirection = playerCamera.transform.forward;

        for (int i = 0; i < playerLv; i++)
        {
            // ���ˈʒu�Ƀ����_���Ȍ덷��ǉ��i�㉺���E�j
            Vector3 randomOffset = new Vector3(
                Random.Range(-delta, delta),  // X�������̌덷
                Random.Range(-delta, delta),  // Y�������̌덷
                Random.Range(-delta, delta)   // Z�������̌덷�i�K�v�ł���΁j
            );


            // ���ˏꏊ������
            Vector3 spawnPosition = laserSpawnPoint.position + randomOffset;

            // ���@�̒e�𔭎�
            GameObject magicShot = Instantiate(magicPrefab, spawnPosition, transform.rotation);

            magicShot.transform.forward = shootDirection + (randomOffset * 0.1f);


            // 0.1�b�ҋ@���Ď��̒e�𔭎�
            yield return new WaitForSeconds(shotInterval);
        }
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
        int laserCount = 0;

        foreach (Transform target in targets)
        {
            if (laserCount > playerLv)    break;  // Lv�ɂ�锭�ː�����

            Debug.Log("LLL");

            // Player�̎q�I�u�W�F�N�g�Ƃ��Đ����iStatusManager��Pearent�ŎQ�Ƃ��邽�߁j
            GameObject laser = Instantiate(laserPrefab, laserSpawnPoint.position, Quaternion.identity,transform);
            SimpleLaser simpleLaser = laser.GetComponent<SimpleLaser>();
            if (simpleLaser != null)
            {
                simpleLaser.SetTarget(target); // �^�[�Q�b�g��ݒ�
            }

            laserCount++;
        }
    }

    // �f�o�b�O�p�ɔ͈͂���������
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void GetLv()
    {
        //�v���C���[���x�����擾�A�e�픭�ː��Ɏg���܂킵
        playerLv = (int)GetComponent<StatusManager>().ExpRate;
    }
}
