using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    // �V���A���C�Y���ꂽCinemachine�J����
    [SerializeField] private CinemachineVirtualCameraBase mainCamera;   // ���C���J����
    [SerializeField] private CinemachineVirtualCameraBase phase2Camera; // Phase2�p�J����
    [SerializeField] private CinemachineVirtualCameraBase phase3Camera; // Phase3�p�J����
    [SerializeField] private CinemachineVirtualCameraBase phase4Camera; // Phase4�p�J����

    private Coroutine switchBackCoroutine; // ���C���J�����ɖ߂鏈���p�R���[�`��

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
    /// �w�肳�ꂽ�t�F�[�Y�̃J�����ɐ؂�ւ���
    /// </summary>
    /// <param name="phase">�؂�ւ������t�F�[�Y�ԍ�</param>
    public void SwitchToPhaseCamera(int phase)
    {
        // ���݂̃R���[�`�����~�i���ɖ߂鏈�����̏ꍇ���l���j
        if (switchBackCoroutine != null)
        {
            StopCoroutine(switchBackCoroutine);
        }

        // �S�ẴJ�����̗D��x�����Z�b�g
        mainCamera.Priority = 0;
        phase2Camera.Priority = 0;
        phase3Camera.Priority = 0;
        phase4Camera.Priority = 0;

        // �w��t�F�[�Y�̃J�������A�N�e�B�u��
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
                return; // �f�t�H���g�̎��͖߂�K�v���Ȃ����߂����ŏI��
        }

        // 3�b��Ƀ��C���J�����ɖ߂��R���[�`�����J�n
        switchBackCoroutine = StartCoroutine(SwitchBackToMainCamera());
    }

    /// <summary>
    /// 3�b��Ƀ��C���J�����֖߂�����
    /// </summary>
    private IEnumerator SwitchBackToMainCamera()
    {
        yield return new WaitForSeconds(3f);

        CameraReset();

        switchBackCoroutine = null; // �R���[�`�����I���������Ƃ��L�^
    }

    public void CameraReset()
    {
        // ���C���J�������A�N�e�B�u�ɂ���
        mainCamera.Priority = 10;

        // ���̃J�������A�N�e�B�u��
        phase2Camera.Priority = 0;
        phase3Camera.Priority = 0;
        phase4Camera.Priority = 0;
    }
}
