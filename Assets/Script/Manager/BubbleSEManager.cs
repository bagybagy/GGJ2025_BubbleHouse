using UnityEngine;

public class BubbleSEManager : MonoBehaviour
{
    public AudioClip seClip;   // 再生する効果音のクリップ
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();  // AudioSourceを追加
        audioSource.clip = seClip;  // 効果音の設定
        audioSource.playOnAwake = false; // PlayOnAwakeを無効化

        PlaySeWithChangingPitch();
    }

    // ピッチをランダムに変えて効果音を再生
    public void PlaySeWithChangingPitch()
    {
        // ランダムにピッチを変更（例: 1.0〜2.0の間で）
        audioSource.pitch = Random.Range(0.5f, 1.3f);

        // ピッチ変更後、効果音を再生
        audioSource.PlayOneShot(seClip);
    }
}