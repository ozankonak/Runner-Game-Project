using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioManager.instance.PlayCoinSound();
            ScoreManager.instance.Coins++;
            UIManager.instance.coinText.text = ScoreManager.instance.Coins.ToString();
            Destroy(gameObject);
        }
    }
}
