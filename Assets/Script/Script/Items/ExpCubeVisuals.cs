// ExpCubeVisuals.cs (見た目の部分)
using UnityEngine;

public class ExpCubeVisuals : MonoBehaviour
{
    [SerializeField] float floatHeight = 0.5f; // 浮遊の高さ（上下に動く範囲）
    [SerializeField] float floatSpeed = 1f;    // 浮遊の速さ（Sin波の変化速度）
    [SerializeField] float floatOffsetY = 0.5f;    // 浮遊の速さ（Sin波の変化速度）


    [SerializeField] float rotationSpeed = 10f; // 回転速度（y軸の回転量）

    void Update()
    {
        // === 上下浮遊の動作 ===
        // ローカル空間基準で上下浮遊させる
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.localPosition = new Vector3(0, newY + floatOffsetY, 0); // ローカル空間で上下動

        // === 回転の動作 ===
        float newRot = Time.time * rotationSpeed; 

        // 回転角度をQuaternion.Eulerで設定
        transform.rotation = Quaternion.Euler(newRot, 0, newRot);
    }
}
