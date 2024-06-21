using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ƒVƒ“ƒOƒ‹ƒgƒ“
public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource = default;
    [SerializeField] AudioClip[] audioClips = default;

    public enum BGM
    {
        Title,
        CharaSelect,
        Game,
        Result
    }

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Sound(BGM bgm)
    {
        audioSource.clip = audioClips[(int)bgm];
        audioSource.Play();
    }
}
