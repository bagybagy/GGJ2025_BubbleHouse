using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private float rotationDuration = 2f; // 回転にかける時間（秒）

    private IEnumerator RotateOverTime(Quaternion rotationOffset)
    {
        Quaternion startRotation = transform.rotation; // 現在の回転
        Quaternion targetRotation = startRotation * rotationOffset; // 目標回転

        float elapsedTime = 0f;

        // 指定時間かけて回転
        while (elapsedTime < rotationDuration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // 次のフレームまで待機
        }

        // 回転を正確に目標に設定
        transform.rotation = targetRotation;
    }
    public void Open()
    {
        // コルーチンを開始して90度回転
        StartCoroutine(RotateOverTime(Quaternion.Euler(0, 90, 0)));
    }

}
