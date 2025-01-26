using UnityEngine;
using System.Collections;

public class EnemyActionshoot : MonoBehaviour, IEnemyAction
{
    [SerializeField] GameObject bulletPrefab; // �e��Prefab�i�e�e�Ȃǁj���w��
    [SerializeField] Transform firePoint; // �e�𔭎˂���ʒu�i�e���Ȃǁj
    [SerializeField] float fireCooldown = 2f; // �e�𔭎˂���N�[���_�E�����ԁi���ɔ��˂���܂ł̑ҋ@���ԁj
    [SerializeField] Transform target; // �v���C���[�̈ʒu���w��i�ǔ��Ώہj

    private bool isFiring = false; // �e�𔭎˂��Ă��邩�ǂ�����ǐՂ���t���O

    void Start()
    {
        // �Q�[���J�n���Ƀv���C���[�itarget�j�������A�^�[�Q�b�g��ݒ�
        // �v���C���[���V�[���ɂ���O��
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    public void Execute()
    {
        // �e�𔭎˂��鏈�����J�n
        // ���łɔ��˂��Ă��Ȃ���΁A���˃��[�`�����J�n
        if (!isFiring)
        {
            StartCoroutine(FireRoutine());
        }
    }

    public void Cancel()
    {
        // ���˂��L�����Z�����邽�߂̃��\�b�h
        // isFiring��false�ɂ��āA���˂��~����
        isFiring = false;
    }

    private IEnumerator FireRoutine()
    {
        // ���˃��[�`��
        isFiring = true; // ���˒��̃t���O�𗧂Ă�
        while (isFiring)
        {
            // �v���C���[�̕������������������s
            RotateTowardsTarget();

            // �e�𔭎ˁi�w�肵���ʒu�Ńv���C���[�̕������������e�𐶐��j
            Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            Debug.Log("Enemy is firing!"); // �f�o�b�O���b�Z�[�W�i���˂��Ă��邱�Ƃ��m�F�j

            // �N�[���_�E�����Ԃ�ҋ@
            // ���ˌ�Ɏw�肵�����ԑ҂��Ă��玟�̔��˂��s��
            yield return new WaitForSeconds(fireCooldown);
        }
    }

    // �v���C���[�̕�������������
    private void RotateTowardsTarget()
    {
        // �^�[�Q�b�g�i�v���C���[�j�����݂���ꍇ
        if (target != null)
        {
            // �v���C���[�̈ʒu����ɕ������v�Z
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            directionToTarget.y = 0f; // �����ʂ����ŉ�]�𐧌�i���������͖����j

            // �������[���łȂ��ꍇ�i�����ׂ�����������ꍇ�j
            if (directionToTarget != Vector3.zero)
            {
                // �v���C���[�����Ɍ������߂̉�]���v�Z
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                transform.rotation = targetRotation; // �����ɂ��̕���������

                // �ȉ��̃R�����g�A�E�g������L���ɂ���ƁA�X���[�Y�ɉ�]����悤�ɂȂ�܂�
                // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed); // �X���[�Y�Ɍ����ꍇ
            }
        }
    }
}
