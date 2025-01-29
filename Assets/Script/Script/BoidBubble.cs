using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    [SerializeField] private float speed = 2f;                   // �{�C�h�̑��x
    [SerializeField] private float rotationSpeed = 1f;           // ��]�̑���
    [SerializeField] private float neighborRadius = 3f;          // �߂��̃{�C�h�̌��o�͈�
    [SerializeField] private float separationRadius = 1f;        // �����̂��߂̋ߋ����͈�
    [SerializeField] private float alignmentStrength = 1f;       // ����̋���
    [SerializeField] private float cohesionStrength = 0.5f;      // �ÏW�̋���
    [SerializeField] private Vector3 roomSize = new Vector3(10f, 10f, 10f); // �����̃T�C�Y�i���[�J�����W�n�j

    private Vector3 velocity;                                      // �{�C�h�̑��x�x�N�g��
    private List<Boid> neighbors = new List<Boid>();               // �߂��̃{�C�h

    private Transform roomCenter;  // �����̒��S�ʒu�i�e�I�u�W�F�N�g��z��j

    void Start()
    {
        roomCenter = transform.parent;  // �����̐e�I�u�W�F�N�g���擾�i�����̒��S���e�I�u�W�F�N�g�ɐݒ肳��Ă���O��j

        velocity = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * speed;
    }

    void Update()
    {
        FindNeighbors();

        // �e�͂��v�Z�i�����A����A�ÏW�j
        Vector3 separation = Separation() * separationRadius;
        Vector3 alignment = Alignment() * alignmentStrength;
        Vector3 cohesion = Cohesion() * cohesionStrength;

        // �e�͂𐧌�����i�ő呬�x�𒴂��Ȃ��悤�Ɂj
        separation = Vector3.ClampMagnitude(separation, speed);
        alignment = Vector3.ClampMagnitude(alignment, speed);
        cohesion = Vector3.ClampMagnitude(cohesion, speed);

        // ���ꂼ��̗͂����Z���A�ړ��x�N�g�����v�Z
        velocity = Vector3.Lerp(velocity, velocity + separation + alignment + cohesion, 0.1f);

        // ���x���X�V�i�ő呬�x���ێ��j
        velocity = Vector3.ClampMagnitude(velocity, speed);

        // �ړ�
        transform.position += velocity * Time.deltaTime;

        // ��]���X���[�Y�ɂ���
        if (velocity.magnitude > 0.1f)  // ���x���قƂ�ǃ[���łȂ��ꍇ�ɉ�]
        {
            Quaternion targetRotation = Quaternion.LookRotation(velocity);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // �����͈̔͂����[�J�����W�n�Ő���
        LimitPositionWithinRoom();
    }


    // �߂��̃{�C�h�����o
    void FindNeighbors()
    {
        neighbors.Clear();

        // �߂��̃{�C�h��T��
        Boid[] allBoids = FindObjectsOfType<Boid>();
        foreach (Boid boid in allBoids)
        {
            if (boid != this)
            {
                float distance = Vector3.Distance(transform.position, boid.transform.position);
                if (distance < neighborRadius)
                {
                    neighbors.Add(boid);
                }
            }
        }
    }

    // �����F���̃{�C�h�ƏՓ˂��Ȃ��悤��
    Vector3 Separation()
    {
        Vector3 move = Vector3.zero;
        foreach (Boid boid in neighbors)
        {
            float distance = Vector3.Distance(transform.position, boid.transform.position);
            if (distance < separationRadius)
            {
                move += transform.position - boid.transform.position;
            }
        }
        return move;
    }

    // ����F�߂��̃{�C�h�̕����ɍ��킹��
    Vector3 Alignment()
    {
        Vector3 averageVelocity = Vector3.zero;
        foreach (Boid boid in neighbors)
        {
            averageVelocity += boid.velocity;
        }
        if (neighbors.Count > 0)
        {
            averageVelocity /= neighbors.Count;
            return averageVelocity - velocity;
        }
        return Vector3.zero;
    }

    // �ÏW�F���͂̃{�C�h�Ɍ������Đi��
    Vector3 Cohesion()
    {
        Vector3 centerOfMass = Vector3.zero;
        foreach (Boid boid in neighbors)
        {
            centerOfMass += boid.transform.position;
        }
        if (neighbors.Count > 0)
        {
            centerOfMass /= neighbors.Count;
            return (centerOfMass - transform.position).normalized * speed;
        }
        return Vector3.zero;
    }

    // �����̃��[�J���͈͂𐧌�����
    void LimitPositionWithinRoom()
    {
        // �{�C�h�̈ʒu�����[�J�����W�ɕϊ�
        Vector3 localPosition = roomCenter.InverseTransformPoint(transform.position);

        // �����̃T�C�Y����ɁA�{�C�h�̈ʒu�𐧌�
        localPosition.x = Mathf.Clamp(localPosition.x, -roomSize.x / 2, roomSize.x / 2);
        localPosition.y = Mathf.Clamp(localPosition.y, -roomSize.y / 2, roomSize.y / 2);
        localPosition.z = Mathf.Clamp(localPosition.z, -roomSize.z / 2, roomSize.z / 2);

        // ���[�J�����W���烏�[���h���W�ɖ߂��Ĉʒu���X�V
        transform.position = roomCenter.TransformPoint(localPosition);
    }
}
