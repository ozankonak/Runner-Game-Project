using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool isGameStarted = false;
    private bool isGameOver = false;
    private bool isLevelCompleted = false;

    public bool PlayMode => isGameStarted && !isGameOver;

    private void Awake()
    {
        #region SingletonPattern

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        #endregion
    }

    public void StartGame()
    {
        if (!isGameStarted)
        {
            isGameStarted = true;
            FindObjectOfType<PlayerAnimations>().SetIdleAnimation(false);
            AudioManager.instance.PlayStartGameSound();
            UIManager.instance.SetActiveStartGamePanel(false);
        }
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            UIManager.instance.SetActiveGameOverPanel(true);
            AudioManager.instance.PlayGameOverSound();
        }
    }























}
