using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �Q�[���N���A�̏�Ԃ�ێ�����ϐ�
    // �N���A�������ǂ����̃t���O�ł�
    private bool isGameCleared = false;

    // �V���O���g���C���X�^���X
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // �V���O���g���̃Z�b�g�A�b�v
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("GameManager �͕������݂��邱�Ƃ͂ł��܂���I");
            Destroy(gameObject); // �d�������C���X�^���X���폜
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    /// <summary>
    /// �{�X���|���ꂽ�ۂɌĂяo�����֐�
    /// �Q�[���N���A�̔�����s���܂�
    /// </summary>
    public void NotifyBossDefeat()
    {
        // �Q�[���N���A���̏������J�n
        Debug.Log("Game Cleared!");  // �Q�[���N���A�̃��O��\��

        // �Q�[���N���A���̒ǉ������i��: UI�X�V��V�[���J�ڂȂǁj
        HandleGameClear();
    }

    /// <summary>
    /// �Q�[���N���A���Ɏ��s�����̓I�ȏ������܂Ƃ߂����\�b�h
    /// </summary>
    private void HandleGameClear()
    {
        // �Q�[���N���A���ɍs�����������������ɏ����܂�
        Debug.Log("Congratulations! You have cleared the game!");
        // �N���AUI�̌Ăяo��
        UIManager.Instance.ActiveClearUI();

        // ��: �Q�[���N���A��UI��\��������A�V�[����J�ڂ������肷�鏈��
        // SceneManager.LoadScene("GameClearScene"); // �Ⴆ�΁A�Q�[���N���A��ɕʂ̃V�[���ɑJ�ڂ���ꍇ
    }

}
