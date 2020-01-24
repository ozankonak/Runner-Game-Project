using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private AudioSource source;

    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip slideClip;
    private void Awake()
    {
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
}
