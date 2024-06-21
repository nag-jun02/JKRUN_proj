using UnityEngine;
using UniRandom = UnityEngine.Random;

public class Voices : MonoBehaviour
{
    [Header("ダメージボイス")]
    [SerializeField] 
    private AudioClip[] damageVoices;

    [Header("クリアボイス")]
    [SerializeField]
    private AudioClip clearVoice;

    [Header("ゲームオーバーボイス")]
    [SerializeField]
    private AudioClip gameOverVoice;

    AudioSource audioSource;

    Coroutine _Coroutine;

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        _Coroutine = new Coroutine();
    }

    public void DamageVoices()
    {
        int randomIndex;
        //ダメージボイスをdamageAudios配列からランダムに再生
        if (damageVoices != null)
        {
            randomIndex = UniRandom.Range(0, damageVoices.Length);
            audioSource.PlayOneShot(damageVoices[randomIndex]);
        }
    }

    public void GameClearVoice()
    {
        audioSource.PlayOneShot(clearVoice);
    }

    public void GameOverVoice()
    {
        //ゲームオーバーボイスの呼び出しを1.3秒後にする
        StartCoroutine(_Coroutine.DelayCoroutine(1.3f, () =>
        {
            audioSource.PlayOneShot(gameOverVoice);
        }));
    }
}
