using UnityEngine;
using System.Collections;

public class EnemyActionBoss : MonoBehaviour, IEnemyAction
{
    [SerializeField] Transform target;              // Player�̈ʒu���w��
    [SerializeField] float dashSpeed = 10f;         // ���̓ːi���x
    [SerializeField] float dashDuration = 1f;       // �ːi�̌��ʎ���
    [SerializeField] float dashCooldown = 2f;       // �ːi��̑ҋ@����

    private bool isDashing = false;       // �ːi�����ǂ���
    private Rigidbody rb;                 // Rigidbody�R���|�[�l���g
    private Vector3 dashDirection;        // �ːi�̕���

    //�U������i�ߐڕ���j�p�̃R���C�_�[
    [SerializeField]
    Collider attackCollider;

    void Start()
    {
        // Rigidbody�R���|�[�l���g�̎擾
        rb = GetComponent<Rigidbody>();

        // �ǔ��Ώۂ̎擾�iPlayer��z��j
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    // �C���^�[�t�F�C�X�ɂ�鐧��
    public void Execute()
    {
        StartDash();
    }
    public void Cancel()
    {
        CancelDash();
    }

    public void StartDash()
    {
        // �ːi���̏ꍇ�͍Ď��s�����A�x�����b�Z�[�W���o��
        if (isDashing)
        {
            Debug.LogWarning("Dash already in progress.");
            return;
        }

        // ��Ԃ����Z�b�g���Ă���R���[�`�����J�n����
        ResetState();
        // �v���C���[�̕���������
        RotateTowardsTarget();

        StartCoroutine(EnemyDashRoutine());
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
    private IEnumerator EnemyDashRoutine()
    {

        // �ːi���t���O�𗧂Ă�
        isDashing = true;

        // �ːi�������v�Z�i�v���C���[�̕����j
        dashDirection = (target.position - transform.position).normalized;
        dashDirection.y = 0f;   // y���̈ړ��x��0�ɂ��Ēn�ʂƕ��s�ȓːi�ɂ���悤�ɕύX�B�������Ζʂł̓����͖�����

        // �U������p�̃R���C�_�[��L����
        AttackColliderOn();

        // �ːi�I�����Ԃ��v�Z
        float dashEndTime = Time.time + dashDuration;
        // �ːi���̏����idash���ł���΁A�w�肵�����Ԃ����i�ށj
        while (Time.time < dashEndTime && isDashing)
        {
            rb.linearVelocity = dashDirection * dashSpeed; // �ːi�����Ɉ�葬�x�Ői��
            yield return null; // ���̃t���[����
        }

        // �ːi�I����A���x��0�ɂ���
        rb.linearVelocity = Vector3.zero;
        yield return new WaitForSeconds(dashCooldown); // �ҋ@���Ԃ�݂���

        // �ːi�I�����̏��������Z�b�g
        ResetState();
    }

    private void ResetState()
    {
        // �ːi�֘A�̏�Ԃ����Z�b�g���鋤�ʏ���
        isDashing = false;                 // �ːi���t���O�����Z�b�g
        //rb.velocity = Vector3.zero;       // �ːi�̑��x��0�ɖ߂�
        AttackColliderOff();              // �U������p�R���C�_�[�𖳌���
    }

    public void CancelDash()
    {
        // �ːi���łȂ��ꍇ�͉������Ȃ�
        if (!isDashing)
            return;

        // ��Ԃ����Z�b�g���A�f�o�b�O���O���o��
        ResetState();
        Debug.Log("Dash canceled!");
    }

    // �ߐڍU���p�̃R���C�_�[��L���ɂ���֐�
    void AttackColliderOn()
    {
        attackCollider.enabled = true;
        Debug.Log("Attack c on");

    }

    // �ߐڍU���p�̃R���C�_�[�𖳌��ɂ���֐�
    void AttackColliderOff()
    {
        attackCollider.enabled = false;
        Debug.Log("Attack c off");

    }

    public bool IsDashing()
    {
        // �ːi�����ǂ������O������m�F�ł���悤�ɂ���
        return isDashing;
    }
}
