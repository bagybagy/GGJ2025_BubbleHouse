using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public void OnMagicCreate()
    {
        // 親オブジェクトのスクリプトを取得して関数を呼び出す
        Player player = GetComponentInParent<Player>();
        Debug.Log("mmm");
        if (player != null)
        {
            player.MagicCreate();
        }
    }
}
