using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Text coinText;
    [SerializeField] private GameObject startGamePanel;
    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        #region SingletonPattern

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        #endregion
    }

    private void Start()
    {
        SetActiveStartGamePanel(true);
        SetActiveGameOverPanel(false);
    }

    public void SetActiveStartGamePanel(bool value)
    {
        startGamePanel.SetActive(value);
    }

    public void SetActiveGameOverPanel(bool value)
    {
        gameOverPanel.SetActive(value);
    }
}
