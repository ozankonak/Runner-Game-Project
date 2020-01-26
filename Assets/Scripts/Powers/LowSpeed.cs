using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowSpeed : MonoBehaviour
{

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PowerManager.instance.LowSpeed();
            Destroy(gameObject);
        }
    }

}
