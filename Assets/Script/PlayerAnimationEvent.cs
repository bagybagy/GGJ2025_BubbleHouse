using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public void OnMagicCreate()
    {
        // �e�I�u�W�F�N�g�̃X�N���v�g���擾���Ċ֐����Ăяo��
        Player player = GetComponentInParent<Player>();
        Debug.Log("mmm");
        if (player != null)
        {
            player.MagicCreate();
        }
    }
}
