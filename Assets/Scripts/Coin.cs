using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int coins = 0;

    private void Start()
    {
        UIManager.instance.coinText.text = coins.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioManager.instance.PlayCoinSound();
            UIManager.instance.coinText.text = coins.ToString();
            coins++;
            Destroy(gameObject);
        }
    }
}
