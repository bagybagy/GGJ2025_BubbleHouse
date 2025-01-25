using UnityEngine;
using System.Collections;

public class BigJumpObject : MonoBehaviour
{
    [SerializeField] private float jumpForce = 20f;  // プレイヤーに加えるジャンプ力
    [SerializeField] private float sinkTime = 0.3f;  // 沈み込む時間
    [SerializeField] private float sinkAmount = 0.5f;  // 沈み込む量（0～1の割合で設定）
    [SerializeField] private float returnSpeed = 0.3f;  // 元の位置に戻るスムーズさ
    [SerializeField] private float jumpWaitTime = 0.1f;  // ジャンプ後の待機時間

    private bool isPlayerOnTop = false;  // プレイヤーが乗っているかどうか
    private bool canJump = true;  // ジャンプ可能かどうか
    private Vector3 velocity = Vector3.zero;  // SmoothDampの速度を保持するための変数

    private void OnCollisionEnter(Collision collision)
    {
        // プレイヤーがオブジェクトに乗ったとき
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnTop = true;

            // プレイヤーのRigidbodyを取得してジャンプ処理を実行
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null && canJump)
            {
                // ジャンプオブジェクトが沈み込む
                StartCoroutine(SinkAndJump(collision.gameObject));
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // プレイヤーが降りたとき
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnTop = false;
            canJump = true;  // 次に乗ったときにジャンプできるようにする
        }
    }

    // コルーチンで沈み込みとジャンプの動作を制御
    private IEnumerator SinkAndJump(GameObject player)
    {
        // 沈み込み開始
        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = originalPosition - new Vector3(0f, sinkAmount, 0f);

        // 沈み込み処理
        float elapsedTime = 0f;
        while (elapsedTime < sinkTime)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / sinkTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;  // 最終的に沈み込む

        // 少し待ってから反発させる（0.1秒くらい）
        yield return new WaitForSeconds(jumpWaitTime);

        // プレイヤーを大ジャンプさせる
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
        if (playerRb != null)
        {
            playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, 0f, playerRb.linearVelocity.z);  // 垂直速度をリセット
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  // 上方向にジャンプ力を加える
        }

        // ジャンプオブジェクトが元の位置に戻る処理（SmoothDampで補完）
        float smoothDampTime = 0.5f;
        while ((transform.position - originalPosition).magnitude > 0.01f)  // 目標位置に近づくまでループ
        {
            transform.position = Vector3.SmoothDamp(transform.position, originalPosition, ref velocity, returnSpeed);
            yield return null;
        }
        transform.position = originalPosition;  // 最終的に元の位置に戻す
    }
}