using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int Coins { get; set; } = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UIManager.instance.coinText.text = Coins.ToString();
    }
}
