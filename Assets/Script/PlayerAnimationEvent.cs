using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    // 親オブジェクトのスクリプトを取得して関数を呼び出す
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
