using System.Collections;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    private int currentBubbleCount = 0; // ���݂�Bubble��
    [SerializeField] private bool isExplorationPhase = false; // �T���t�F�[�Y�̐���t���O

    // ��������̎Q�Ɨp
    public int CurrentBubbleCount
    {
        get { return currentBubbleCount; } 
    }
    public bool IsExplorationPhase
    {
        get { return isExplorationPhase; }
    }

    // �V���O���g���C���X�^���X
    public static BubbleManager Instance { get; private set; }

    private void Awake()
    {
        // �V���O���g���̃Z�b�g�A�b�v
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("BubbleManager �͕������݂��邱�Ƃ͂ł��܂���I");
            Destroy(gameObject); // �d�������C���X�^���X���폜
        }
    }

    private void Start()
    {
        // Bubble�̐������I�Ƀ`�F�b�N����R���[�`�����J�n
        StartCoroutine(CheckBubbleCountRoutine());
    }

    // Bubble�̐������I�Ɋm�F����R���[�`��
    private IEnumerator CheckBubbleCountRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // 1�b���ƂɊm�F�i�K�v�ɉ����Ē����j

            // �f�o�b�O�p���O
            Debug.Log("Current Bubble Count: " + currentBubbleCount); // ���݂�Bubble�������O�ɏo��

            // Bubble�^�O�����I�u�W�F�N�g�̐����擾(.Length�Ő����擾�ł���)
            currentBubbleCount = GameObject.FindGameObjectsWithTag("Bubble").Length;
            if (currentBubbleCount <= 0 && !isExplorationPhase)
            {
                isExplorationPhase = true;  // �T���t�F�[�Y�ɐ؂�ւ�

                Debug.Log("Bubble Clear");   //Bubble�����ׂē|���ꂽ����A���o��t�F�[�Y�ύX�������Ă���

                // Bubble���ēx�o������܂őҋ@
                while (currentBubbleCount <= 0)
                {
                    currentBubbleCount = GameObject.FindGameObjectsWithTag("Bubble").Length; // Bubble�̐����Ċm�F
                    yield return new WaitForSeconds(1f); // 1�b���ƂɍĊm�F
                }

                isExplorationPhase = false; // �ēxBubble���o��������T���t�F�[�Y���I��
            }
        }
    }
}