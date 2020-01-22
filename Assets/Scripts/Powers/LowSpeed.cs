using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowSpeed : MonoBehaviour
{

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Time.timeScale = 0.5f;

            Destroy(gameObject);

            Invoke("SetSpeed", 2f);
        }
    }

    void SetSpeed()
    {
        Time.timeScale = 1f;
    }













}
