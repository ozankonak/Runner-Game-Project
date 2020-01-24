using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Forward, Back, Right, Left }
public class PlayerMovement : MonoBehaviour
{
    private CharacterController charController;
    private PlayerPosition playerPos;

    [HideInInspector]
    public  Direction direction;

    private float speed = 20f;
    private Vector3 moveDirection = Vector3.zero;

    //For Jump
    private float jumpSpeed = 10f;
    private float gravity = 20.0f;
    //For Slide
    private float defaultHeight;
    private Vector3 defaultCenter;
    private bool canSlide = true;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        playerPos = GetComponent<PlayerPosition>();
    }

    private void Start()
    {
        defaultHeight = charController.height;
        defaultCenter = charController.center;
        direction = Direction.Forward;
    }

    private void Update()
    {
        Move();
        Gravity();
    }

    private void Gravity()
    {
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        charController.Move(moveDirection * Time.deltaTime);
    }
    private void Move()
    {
        if (direction == Direction.Forward)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            charController.Move(Vector3.forward * speed * Time.deltaTime);
            if (playerPos.playerPosition == Position.Middle) 
                transform.position = new Vector3(playerPos.MiddlePoint.x, transform.position.y, transform.position.z); 
            else if (playerPos.playerPosition == Position.Left) 
                transform.position = new Vector3(playerPos.LeftPoint.x, transform.position.y, transform.position.z); 
            else if (playerPos.playerPosition == Position.Right)
                transform.position = new Vector3(playerPos.RightPoint.x, transform.position.y, transform.position.z);
        
        }

        else if (direction == Direction.Right)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            charController.Move(Vector3.right * speed * Time.deltaTime);
            if (playerPos.playerPosition == Position.Middle) 
                transform.position = new Vector3(transform.position.x, transform.position.y, playerPos.MiddlePoint.z); 
            else if (playerPos.playerPosition == Position.Left) 
                transform.position = new Vector3(transform.position.x, transform.position.y, playerPos.LeftPoint.z); 
            else if (playerPos.playerPosition == Position.Right) 
                transform.position = new Vector3(transform.position.x, transform.position.y, playerPos.RightPoint.z); 
        }

        else if (direction == Direction.Left)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            charController.Move(Vector3.left * speed * Time.deltaTime);
            if (playerPos.playerPosition == Position.Middle)  
                transform.position = new Vector3(transform.position.x, transform.position.y, playerPos.MiddlePoint.z); 
            else if (playerPos.playerPosition == Position.Left)
                transform.position = new Vector3(transform.position.x, transform.position.y, playerPos.LeftPoint.z); 
            else if (playerPos.playerPosition == Position.Right)  
                transform.position = new Vector3(transform.position.x, transform.position.y, playerPos.RightPoint.z); 
        }
        else if (direction == Direction.Back)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            charController.Move(Vector3.back * speed * Time.deltaTime);
            if (playerPos.playerPosition == Position.Middle) 
                transform.position = new Vector3(playerPos.MiddlePoint.x, transform.position.y, transform.position.z); 
            else if (playerPos.playerPosition == Position.Left) 
                transform.position = new Vector3(playerPos.LeftPoint.x, transform.position.y, transform.position.z); 
            else if (playerPos.playerPosition == Position.Right) 
                transform.position = new Vector3(playerPos.RightPoint.x, transform.position.y, transform.position.z); 
        }
    }

    public void Jump()
    {
        if (charController.isGrounded)
        {
            GetComponent<PlayerSound>().PlayJumpSound();
            GetComponent<PlayerAnimations>().PlayJumpAnimation();
            moveDirection.y = jumpSpeed;
        }
    }

    public void Slide()
    {
        if (charController.isGrounded && canSlide)
        {
            StartCoroutine(DoSlide());
        }
    }

    IEnumerator DoSlide()
    {
        canSlide = false;
        charController.height = charController.height / 2f;
        charController.center = defaultCenter / 2f;
        GetComponent<PlayerSound>().PlaySlideSound();
        GetComponent<PlayerAnimations>().PlaySlideAnimation();
        yield return new WaitForSecondsRealtime(1f);
        charController.height = defaultHeight;
        charController.center = defaultCenter;
        canSlide = true;
    }

    public void RotateToLeft()
    {
        if (direction == Direction.Forward)
                direction = Direction.Left;
            else if (direction == Direction.Back)
                direction = Direction.Right;
            else if (direction == Direction.Right)
                direction = Direction.Forward;
            else if (direction == Direction.Left)
                direction = Direction.Back;
    }

    public void RotateToRight()
    {
        if (direction == Direction.Forward)
                direction = Direction.Right;
        else if (direction == Direction.Back)
                direction = Direction.Left;
        else if (direction == Direction.Right)
                direction = Direction.Back;
        else if (direction == Direction.Left)
                direction = Direction.Forward;
    }
}
