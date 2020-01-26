using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    public static PowerManager instance;

    [SerializeField] private float lowSpeedTime = 10f;
    private bool LowSpeedTouched = false;

    private void Awake()
    {
        instance = this;
    }

    public void LowSpeed()
    {
        Time.timeScale = 0.5f;
        AudioManager.instance.Source.pitch = 0.75f;
        Invoke("LowSpeedEffect",lowSpeedTime / 2f);
    }

    private void LowSpeedEffect()
    {
        AudioManager.instance.Source.pitch = 1f;
        Time.timeScale = 1f;
    }
}
