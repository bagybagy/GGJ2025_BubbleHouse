using UnityEngine;

public class BoosFootSE : MonoBehaviour
{

    public AudioClip seClip;   // 再生する効果音のクリップ
    private AudioSource audioSource;

    [SerializeField] float pichiLow = 0.5f;
    [SerializeField] float pichiHight = 1.3f;
    [SerializeField] float seVolume = 1.0f;
    [SerializeField] private LayerMask groundLayer;       // ターゲットを特定するレイヤー

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = seClip;
        audioSource.volume = seVolume; // 任意の音量値
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("fff");
        // レイヤーマスクが、コライダーのレイヤーを含んでいるかを判定
        if ((groundLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            Debug.Log("Fff");

            PlayFootstep();
        }
    }

    private void PlayFootstep()
    {
        // ランダムにピッチを変更（例: 1.0〜2.0の間で）
        audioSource.pitch = Random.Range(pichiLow, pichiHight);
        // PlayOneShot を使用しつつ音量を反映
        audioSource.PlayOneShot(seClip, audioSource.volume);
    }
}