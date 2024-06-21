using UnityEngine;
using UniRandom = UnityEngine.Random;

public class Voices : MonoBehaviour
{
    [Header("�_���[�W�{�C�X")]
    [SerializeField] 
    private AudioClip[] damageVoices;

    [Header("�N���A�{�C�X")]
    [SerializeField]
    private AudioClip clearVoice;

    [Header("�Q�[���I�[�o�[�{�C�X")]
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
        //�_���[�W�{�C�X��damageAudios�z�񂩂烉���_���ɍĐ�
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
        //�Q�[���I�[�o�[�{�C�X�̌Ăяo����1.3�b��ɂ���
        StartCoroutine(_Coroutine.DelayCoroutine(1.3f, () =>
        {
            audioSource.PlayOneShot(gameOverVoice);
        }));
    }
}
