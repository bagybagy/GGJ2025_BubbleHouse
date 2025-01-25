using UnityEngine;
using System.Collections;

public class EnemyActionshoot : MonoBehaviour, IEnemyAction
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireCooldown = 2f;
    [SerializeField] Transform target;              // Player�̈ʒu���w��

    private bool isFiring = false;

    void Start()
    {
        // �ǔ��Ώۂ̎擾�iPlayer��z��j
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    public void Execute()
    {
        if (!isFiring)
        {
            StartCoroutine(FireRoutine());
        }
    }

    public void Cancel()
    {
        isFiring = false;
    }

    private IEnumerator FireRoutine()
    {
        isFiring = true;
        while (isFiring)
        {
            // �v���C���[�̕���������
            RotateTowardsTarget();

            Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            Debug.Log("Enemy is firing!");
            yield return new WaitForSeconds(fireCooldown);
        }
    }
    // Player�̕�������������
    private void RotateTowardsTarget()
    {
        if (target != null)
        {
            // �v���C���[�̕������v�Z
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            directionToTarget.y = 0f; // �����ʂ����ŉ�]�𐧌�

            // �Ώە���������
            if (directionToTarget != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                transform.rotation = targetRotation; // �����Ɍ����ꍇ
                                                     // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed); // �X���[�Y�Ɍ����ꍇ
            }
        }
    }

}
