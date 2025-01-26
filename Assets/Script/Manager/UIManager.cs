using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Xml.Linq;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField]
    GameObject clearUI;
    [SerializeField]
    GameObject gameOverUI;
    [SerializeField]
    Image hpGage;
    [SerializeField]
    Image expGage;
    [SerializeField]
    private TMP_Text expText;

    private void Awake()
    {
        // Awake�̓I�u�W�F�N�g�����������Ƃ��ɌĂ΂�郁�\�b�h�ł�
        // �����ŃV���O���g���p�^�[�����������Ă��܂�

        // ���ł�GameManager�̃C���X�^���X�����݂��Ă���ꍇ�A���݂̃I�u�W�F�N�g�͔j�����܂�
        if (Instance != null && Instance != this)
        {
            // ���̃C���X�^���X�����݂���ꍇ�A�d�����č��Ȃ��悤�ɃI�u�W�F�N�g��j��
            Destroy(gameObject);
        }
        else
        {
            // GameManager�̃C���X�^���X���Ǘ����邽�߁A�ŏ��ɍ쐬���ꂽ�C���X�^���X��ێ����܂�
            Instance = this;
        }
    }

    public void ActiveClearUI()
    {
        //ckearUI�����݂���ΗL�����A������΃G���[�\��
        if (clearUI != null)
        {
            clearUI.SetActive(true);
        }
        else
        {
            Debug.Log("�N���AUI��������܂���");
        }
    }

    public void LoadGameScene()
    {
        SceneLoader.Instance.LoadSceneWithFade("StageScene");
    }

    // HP�o�[�̍X�V���\�b�h
    public void UpdateHPBar(float currentHP, float maxHP)
    {
        // HP�o�[��FillAmount���X�V
        hpGage.fillAmount = currentHP / maxHP;
        Debug.Log(hpGage.fillAmount);
    }

    public void UpdateExpBar(float currentExp, float borderExp)
    {
        // Exp�o�[��FillAmount���X�V
        expGage.fillAmount = currentExp / borderExp;

        // ���݂̃��x��
        expText.text = $"{(int)currentExp}";
    }

    public void GameOver()
    {
        // ���񂮂�Ƃ񉻂��Ă���e���ŃJ�������Z�b�g���K�v
        CameraManager.Instance.CameraReset();

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        else
        {
            Debug.Log("gameOverUI��������܂���");
        }
        
    }

}
