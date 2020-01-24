using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement playerMov;
    private PlayerPosition playerPos;

    private void Awake()
    {
        playerMov = GetComponent<PlayerMovement>();
        playerPos = GetComponent<PlayerPosition>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            playerMov.Jump();
        if (Input.GetKeyDown(KeyCode.C))
            playerMov.Slide();
        if (Input.GetMouseButtonDown(0))
            playerMov.RotateToLeft();
        if (Input.GetMouseButtonDown(1))
            playerMov.RotateToRight();
        if (Input.GetKeyDown(KeyCode.A))
            playerPos.MoveLeft();
        if (Input.GetKeyDown(KeyCode.D))
            playerPos.MoveRight();
    }
}
