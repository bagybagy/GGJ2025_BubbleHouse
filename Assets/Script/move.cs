using UnityEngine;



// オブジェクトに紐付いている関数
public class move : MonoBehaviour {

    // 更新用の関数
    void Update()
    {


        // ローカル座標取得
        Vector3 posi = this.transform.localPosition;

        float sin = Mathf.Sin(Time.time);

        Debug.Log(sin);
        posi.x =+ sin;
        posi.y =+ sin;
        posi.z =+ sin;

        transform.localPosition = posi;  // 座標を設定
    }

}

