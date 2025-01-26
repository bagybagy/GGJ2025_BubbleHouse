using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // �V���O���g���C���X�^���X
    public static SceneLoader Instance { get; private set; }

    // �t�F�[�h�p��CanvasGroup
    [SerializeField] private Image fadeOverlay;

    // �t�F�[�h�ɂ����鎞��
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        // �C���X�^���X�����łɑ��݂��Ă����玩����j������
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        StartCoroutine(StartSceneWithFade());
    }

    private IEnumerator StartSceneWithFade()
    {
        fadeOverlay.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1f, 0f));  // ��ʂ����X�ɕ\��
        fadeOverlay.gameObject.SetActive(false);    // �t�F�[�h�p�I�[�o�[���C���\��    }
    }

    public void LoadSceneWithFade(string sceneName)
    {
        // �t�F�[�h�ƃV�[�����[�h�����s
        StartCoroutine(FadeAndLoad(sceneName));
    }

    private IEnumerator FadeAndLoad(string sceneName)
    {
        fadeOverlay.gameObject.SetActive(true);     // �t�F�[�h�p�I�[�o�[���C��\��
        yield return StartCoroutine(Fade(0f, 1f));       // ��ʂ����X�ɔ�������

        SceneManager.LoadScene(sceneName);          // �V�[�������[�h����
    }

    private IEnumerator Fade(float startAlpha, float targetAlpha)
    {
        // ���݂̓����x���擾
        Color currentColor = fadeOverlay.color;
        currentColor.a = startAlpha;
        fadeOverlay.color = currentColor;

        // ���Ԍo�߂ɉ����ē����x��ύX
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            currentColor.a = Mathf.Lerp(startAlpha, targetAlpha, t / fadeDuration);
            fadeOverlay.color = currentColor;  // �A���t�@�l��ύX
            yield return null; // �t���[���ҋ@
        }
    }

}
