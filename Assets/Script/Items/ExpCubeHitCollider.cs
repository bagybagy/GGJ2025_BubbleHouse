using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCubeHitCollider : MonoBehaviour
{
    // ExpCubeへの参照
    private ExpCube expCube;
    void Start()
    {
        // 親オブジェクトの StatusManager を取得
        expCube = GetComponentInParent<ExpCube>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"ExpCubeコライダー検知！");
            // Playerに接触したら、ExpCubeの処理を呼び出す
            expCube.GetExpCube(other); // PlayerのStatusManagerを渡す
        }
    }
}
