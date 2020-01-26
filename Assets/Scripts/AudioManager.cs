using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource Source { get; set; }

    [SerializeField] private AudioClip coinClip = null;
    [SerializeField] private AudioClip jumpClip = null;
    [SerializeField] private AudioClip slideClip = null;
    private void Awake()
    {
        instance = this;
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

}
