using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;




public class StatusManager : MonoBehaviour
{
    [SerializeField] GameObject MainObject; //���̃X�N���v�g���A�^�b�`����I�u�W�F�N�g
    [SerializeField] int hp = 1;             //hp���ݒl
    [SerializeField] int maxHp = 1;          //������maxHp���p����ۂɎg�p

    [SerializeField] int attackDamage = 10; // ���̃I�u�W�F�N�g�̍U����

    [SerializeField] GameObject destroyEffect;  //���j�G�t�F�N�g
    [SerializeField] GameObject damageEffect;   //��e�G�t�F�N�g

    // Update is called once per frame
    void Update()
    {
        //hp��0�ȉ��Ȃ�A���j�G�t�F�N�g�𐶐�����Main��j��
        if (hp <= 0)
        {
            DestoryMainObject();
        }
    }

    // ��_���[�W���̏���
    public void Damage(int baseDamage, float takeCrit, Vector3 attackPoint)
    {
        Debug.Log("damage");
        float finalDamage;  // �v�Z��̍ŏI�_���[�W

        // �_���[�W�v�Z���p�֐���
        DamageCalc(baseDamage, out finalDamage);

        // HP������
        hp -= Mathf.RoundToInt(finalDamage);

        var effect = Instantiate(damageEffect);     // �_���[�W�G�t�F�N�g�̐���
        effect.transform.position = attackPoint; // �_���[�W�G�t�F�N�g�̐����ꏊ�̎w��

        HPGageUpdateUI();

    }

    private void HPGageUpdateUI()
    {
        // UIManager ��HP�X�V��ʒm
        if (this.gameObject.CompareTag("Player")) // �v���C���[�̏ꍇ�ɂ̂�UI�X�V
        {
            UIManager.Instance.UpdateHPBar(hp, maxHp);
        }
    }

    private void DamageCalc(int baseDamage,out float finalDamage)
    {
        // �����_���␳���v�Z�i-20%�`+20%�͈̔́j
        float randomFactor = Random.Range(0.8f, 1.2f);
        finalDamage = baseDamage * randomFactor;
    }

    // �A�^�b�`���ꂽ�I�u�W�F�N�g���j�󂳂��ۂ̏���
    private void DestoryMainObject()
    {
        hp = 0;
        var effect = Instantiate(destroyEffect);
        effect.transform.position = transform.position;
        Destroy(effect, 5);

        // DropTable���擾���ăA�C�e���h���b�v
        DropTable dropTable = GetComponentInChildren<DropTable>();
        if (dropTable != null)
        {
            dropTable.DropItems();
        }

        Destroy(MainObject);
    }

    // �U���͂�Ԃ��֐�
    public int GetDamageAmount()
    {
        return attackDamage;
    }

}
