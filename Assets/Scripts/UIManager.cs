using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Text coinText;

    private void Awake()
    {
        instance = this;
    }
}
