using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource Source { get; set; }

    [SerializeField] private AudioClip coinClip = null;
    [SerializeField] private AudioClip jumpClip = null;
    [SerializeField] private AudioClip slideClip = null;
    [SerializeField] private AudioClip startGameClip = null;
    [SerializeField] private AudioClip playGameOverClip = null;

    private void Awake()
    {
        #region SingletonPattern

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        #endregion

        Source = GetComponent<AudioSource>();
    }

    public void PlayJumpSound()
    {
        Source.PlayOneShot(jumpClip);
    }

    public void PlaySlideSound()
    {
        Source.PlayOneShot(slideClip);
    }

    public void PlayCoinSound()
    {
        Source.PlayOneShot(coinClip);
    }

    public void PlayStartGameSound()
    {
        Source.PlayOneShot(startGameClip);
    }

    public void PlayGameOverSound()
    {
        Source.PlayOneShot(playGameOverClip);
    }
}
