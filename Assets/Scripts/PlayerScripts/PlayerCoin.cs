using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoin : MonoBehaviour {

    [SerializeField] Text coinText;
    private int coins = 0;
    private AudioSource source;

    [SerializeField] AudioClip coinClip;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        coinText.text = coins.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            source.PlayOneShot(coinClip);
            coins++;
            coinText.text = coins.ToString();
            Destroy(other.gameObject);
        }
    }
}
