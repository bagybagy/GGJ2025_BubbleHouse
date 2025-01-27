using UnityEngine;

public class ChangePichSEManager : MonoBehaviour
{
    public AudioClip seClip;   // 再生する効果音のクリップ
    private AudioSource audioSource;

    [SerializeField] float pichiLow = 0.5f;
    [SerializeField] float pichiHight = 1.3f;
    [SerializeField] float seVolume = 1.0f;

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
        audioSource.pitch = Random.Range(pichiLow, pichiHight);

        audioSource.volume = seVolume; // 任意の音量値
        // PlayOneShot を使用しつつ音量を反映
        audioSource.PlayOneShot(seClip, audioSource.volume);
    }
}