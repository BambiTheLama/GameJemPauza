using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [Header("Sources")]
    [SerializeField] AudioSource bgMusicSource;
    [SerializeField] AudioSource fxMusicSource;


    [Header("Sounds")]
    public AudioClip background;
    public AudioClip win;
    public AudioClip lose;

    private void Awake()
    {
        AudioManager existingAudioManager = FindObjectOfType<AudioManager>();

        if (existingAudioManager != null && existingAudioManager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        bgMusicSource.clip = background;
        bgMusicSource.Play();
    }

    public void PlayFX(AudioClip clip)
    {
        fxMusicSource.PlayOneShot(clip);
    }    

}
