using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

//参考にしやすくするため、InputSystemTutorialとできるだけ変数名等を同一に作成します

public class Player : MonoBehaviour
{
    //移動速度
    [SerializeField]
    private float moveSpeed = 5f;
    // ジャンプ力
    [SerializeField]
    private Vector3 jumpForce = new(0, 5f, 0);

    // Move アクションの入力値[-1.0, 1.0f]
    Vector2 moveInput = Vector2.zero;

    // コンポーネントを事前に参照しておく変数
    new Rigidbody rigidbody;

    //アニメーション用のアニメーターを宣言（Startで取得）
    Animator playerAnimator;
    //走り判定用のbool
    bool isRun = false;

    [SerializeField]
    GameObject magicPrefab;
    [SerializeField]
    GameObject spawnerPoint;
    // 攻撃用火器管制
    [SerializeField]
    FireControllSystem fireControllSystem;
    // 死亡フラグ
    [SerializeField] private bool deadFlag = false;

    // 地面判定用のRayの長さ
    [SerializeField]
    private float groundCheckDistance = 0.2f; // プレイヤーの足元から下に向けてレイを飛ばす距離

    // ジャンプするために地面に接触しているかを判定するフラグ
    [SerializeField]
    private bool isGrounded;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Move();
        CheckGrounded();
    }

    // 指定した速度で、このキャラクターを移動させます。
    public void Move()
    {
        if (Camera.main != null)
        {
            // メインカメラの前方と右方向を取得（カメラローカル座標でいうところのz軸方向とx軸方向）
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;

            // カメラのy軸方向を無視して、地面に沿った移動にする
            cameraForward.y = 0;
            cameraRight.y = 0;

            // キャラクターの角速度をリセット
            rigidbody.angularVelocity = Vector3.zero;

            // 正規化して、カメラの前方と右方向に基づいた移動ベクトルを計算
            Vector3 moveDirection = (cameraForward * moveInput.y + cameraRight * moveInput.x).normalized;

            // moveInputは2Dベクトルで、プレイヤーの移動入力を表します。
            // moveInput.x: 左右の移動入力（-1.0f は左、1.0f は右）
            // moveInput.y: 前後の移動入力（-1.0f は後退、1.0f は前進）
            // 注意: このmoveInput.yは、ジョイスティックやキーボード入力の前後の動きであり、
            //       3D空間のY軸（上下方向）とは異なります。
            //       3D空間のY軸は、物理的な上下移動（ジャンプや落下など）を示します。

            // 移動ベクトルに速度を掛けて移動
            rigidbody.linearVelocity = moveDirection * moveSpeed + new Vector3(0, rigidbody.linearVelocity.y, 0);

            // キャラクターを移動する方向に向かせるための処理
            if (moveDirection != Vector3.zero && rigidbody.linearVelocity.magnitude > 0.1f)  // 何かしら移動が発生している場合のみ回転させる
            {
                // Quaternion.LookRotationは、指定された方向（moveDirection）を向くための回転を計算します。
                // moveDirectionはカメラの向きに基づいた移動方向です。
                // つまり、キャラクターが進む方向に合わせてキャラクターの向きを変えるための回転を求めています。
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

                // transform.rotationはキャラクターの現在の回転を表します。
                // Quaternion.Slerpは、現在の回転（transform.rotation）から目標の回転（targetRotation）までを滑らかに補間します。
                // Time.deltaTime * 10fは、補間の速度を決めるためのものです。値が大きいほど速く回転し、小さいほどゆっくり回転します。
                // この補間処理によって、キャラクターは急に向きを変えるのではなく、自然な速度で回転します。
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

                //移動中判定なので、アニメーション用のフラグをtrueにする
                isRun = true;

            }
            else
            {
                //移動中じゃなければフラグを下ろす
                isRun = false;
            }

            //AnimatorにisRunの状態を送る
            playerAnimator.SetBool("Run", isRun);

        }



    }

    // このキャラクターをジャンプさせます。
    public void Jump()
    {
        if (isGrounded)
        {
            rigidbody.AddForce(jumpForce, ForceMode.Impulse);
            playerAnimator.SetTrigger("Jump"); // アニメーションのトリガーをセット
        }
    }

    // 足元にRayを飛ばして地面に接触しているかを確認する
    private void CheckGrounded()
    {
        // 足元から少し上（例えば、足元から0.1f程度上）にRayを発射
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down); // 足元少し上にRayを発射
        RaycastHit hit;

        // レイが地面（または指定したレイヤー）に当たったかを判定
        if (Physics.Raycast(ray, out hit, groundCheckDistance))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) // "Ground"レイヤーに接触していれば
            {
                isGrounded = true; // 地面にいるのでジャンプ可能
            }
        }
        else
        {
            isGrounded = false; // 地面にいない場合はジャンプ不可
        }
    }

    // Move アクションによって呼び出されます。
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Jump アクションによって呼び出されます。
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Jump();
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Fire();
        }
    }

    private void Fire()
    {
        playerAnimator.SetTrigger("Magic");
    }

    public void MagicCreate()
    {
        fireControllSystem.LinearMagicShot();
    }

    public void OnMultiMagic(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            MultiMagic();
        }
    }

    private void MultiMagic()
    {
        playerAnimator.SetTrigger("MultiMagic");
    }

    public void MultiMagicCreate()
    {
        Debug.Log("ml");
        fireControllSystem.MultiMagicLaser();
    }

    public void PlayerDead()
    {
        if (!deadFlag)
        {
            playerAnimator.SetTrigger("Dead");
        }
        deadFlag = true;

    }
    public void GameOverUIShow()
    {
        UIManager.Instance.GameOver();
    }
}
