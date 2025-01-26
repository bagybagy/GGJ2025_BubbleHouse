using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // シングルトンインスタンス
    public static SceneLoader Instance { get; private set; }

    // フェード用のCanvasGroup
    [SerializeField] private Image fadeOverlay;

    // フェードにかける時間
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        // インスタンスがすでに存在していたら自分を破棄する
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
        yield return StartCoroutine(Fade(1f, 0f));  // 画面を徐々に表示
        fadeOverlay.gameObject.SetActive(false);    // フェード用オーバーレイを非表示    }
    }

    public void LoadSceneWithFade(string sceneName)
    {
        // フェードとシーンロードを実行
        StartCoroutine(FadeAndLoad(sceneName));
    }

    private IEnumerator FadeAndLoad(string sceneName)
    {
        fadeOverlay.gameObject.SetActive(true);     // フェード用オーバーレイを表示
        yield return StartCoroutine(Fade(0f, 1f));       // 画面を徐々に白くする

        SceneManager.LoadScene(sceneName);          // シーンをロードする
    }

    private IEnumerator Fade(float startAlpha, float targetAlpha)
    {
        // 現在の透明度を取得
        Color currentColor = fadeOverlay.color;
        currentColor.a = startAlpha;
        fadeOverlay.color = currentColor;

        // 時間経過に応じて透明度を変更
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            currentColor.a = Mathf.Lerp(startAlpha, targetAlpha, t / fadeDuration);
            fadeOverlay.color = currentColor;  // アルファ値を変更
            yield return null; // フレーム待機
        }
    }

}
