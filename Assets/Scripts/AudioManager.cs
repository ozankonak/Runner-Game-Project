using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioSource source;

    [SerializeField] private AudioClip coinClip = null;
    [SerializeField] private AudioClip jumpClip = null;
    [SerializeField] private AudioClip slideClip = null;
    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlayJumpSound()
    {
        source.PlayOneShot(jumpClip);
    }

    public void PlaySlideSound()
    {
        source.PlayOneShot(slideClip);
    }

    public void PlayCoinSound()
    {
        source.PlayOneShot(coinClip);
    }

}
