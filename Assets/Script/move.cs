using UnityEngine;



// �I�u�W�F�N�g�ɕR�t���Ă���֐�
public class move : MonoBehaviour {

    // �X�V�p�̊֐�
    void Update()
    {


        // ���[�J�����W�擾
        Vector3 posi = this.transform.localPosition;

        float sin = Mathf.Sin(Time.time);

        Debug.Log(sin);
        posi.x =+ sin;
        posi.y =+ sin;
        posi.z =+ sin;

        transform.localPosition = posi;  // ���W��ݒ�
    }

}

