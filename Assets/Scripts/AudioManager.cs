using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioSource source;

    [SerializeField] private AudioClip coinClip;
    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlayCoinSound()
    {
        source.PlayOneShot(coinClip);
    }

}
