using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    // �e�I�u�W�F�N�g�̃X�N���v�g���擾���Ċ֐����Ăяo��
    Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    public void OnMagicCreate()
    {
        if (player != null)
        {
            player.MagicCreate();
        }
    }

    public void OnMultiMagicCreate()
    {
        if (player != null)
        {
            player.MultiMagicCreate();
        }
    }


}
