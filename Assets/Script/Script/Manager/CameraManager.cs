using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    // シリアライズされたCinemachineカメラ
    [SerializeField] private CinemachineVirtualCameraBase mainCamera;   // メインカメラ
    [SerializeField] private CinemachineVirtualCameraBase phase2Camera; // Phase2用カメラ
    [SerializeField] private CinemachineVirtualCameraBase phase3Camera; // Phase3用カメラ
    [SerializeField] private CinemachineVirtualCameraBase phase4Camera; // Phase4用カメラ

    private Coroutine switchBackCoroutine; // メインカメラに戻る処理用コルーチン

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// 指定されたフェーズのカメラに切り替える
    /// </summary>
    /// <param name="phase">切り替えたいフェーズ番号</param>
    public void SwitchToPhaseCamera(int phase)
    {
        // 現在のコルーチンを停止（既に戻る処理中の場合を考慮）
        if (switchBackCoroutine != null)
        {
            StopCoroutine(switchBackCoroutine);
        }

        // 全てのカメラの優先度をリセット
        mainCamera.Priority = 0;
        phase2Camera.Priority = 0;
        phase3Camera.Priority = 0;
        phase4Camera.Priority = 0;

        // 指定フェーズのカメラをアクティブ化
        switch (phase)
        {
            case 2:
                phase2Camera.Priority = 10;
                break;
            case 3:
                phase3Camera.Priority = 10;
                break;
            case 4:
                phase4Camera.Priority = 10;
                break;
            default:
                mainCamera.Priority = 10;
                return; // デフォルトの時は戻る必要がないためここで終了
        }

        // 3秒後にメインカメラに戻すコルーチンを開始
        switchBackCoroutine = StartCoroutine(SwitchBackToMainCamera());
    }

    /// <summary>
    /// 3秒後にメインカメラへ戻す処理
    /// </summary>
    private IEnumerator SwitchBackToMainCamera()
    {
        yield return new WaitForSeconds(3f);

        CameraReset();

        switchBackCoroutine = null; // コルーチンが終了したことを記録
    }

    public void CameraReset()
    {
        // メインカメラをアクティブにする
        mainCamera.Priority = 10;

        // 他のカメラを非アクティブ化
        phase2Camera.Priority = 0;
        phase3Camera.Priority = 0;
        phase4Camera.Priority = 0;
    }
}
