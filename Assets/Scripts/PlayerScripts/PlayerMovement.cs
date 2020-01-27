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
        if (!GameManager.instance.PlayMode)
            return;

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
            ChangeRotation(0,Vector3.forward);

            ChangePosition(playerPos.MiddlePoint.x,transform.position.z,
                playerPos.LeftPoint.x,transform.position.z,playerPos.RightPoint.x,transform.position.z);
        }

        else if (direction == Direction.Right)
        {
            ChangeRotation(90, Vector3.right);

            ChangePosition(transform.position.x, playerPos.MiddlePoint.z,
                transform.position.x, playerPos.LeftPoint.z, transform.position.x, playerPos.RightPoint.z);
        }
        else if (direction == Direction.Left)
        {
            ChangeRotation(-90,Vector3.left);

            ChangePosition(transform.position.x,playerPos.MiddlePoint.z,
                transform.position.x,playerPos.LeftPoint.z,transform.position.x,playerPos.RightPoint.z);
        }
        else if (direction == Direction.Back)
        {
            ChangeRotation(180,Vector3.back);

            ChangePosition(playerPos.MiddlePoint.x,transform.position.z,
                playerPos.LeftPoint.x,transform.position.z,playerPos.RightPoint.x,transform.position.z);
        }
    }

    private void ChangeRotation(float rotation, Vector3 pos)
    {
        transform.rotation = Quaternion.Euler(0, rotation, 0);
        charController.Move(pos * speed * Time.deltaTime);
    }

    private void ChangePosition(float middleX,float middleZ,float leftX,float leftZ, float rightX,float rightZ)
    {
        if (playerPos.playerPosition == Position.Middle)
            transform.position = new Vector3(middleX, transform.position.y, middleZ);
        else if (playerPos.playerPosition == Position.Left)
            transform.position = new Vector3(leftX, transform.position.y, leftZ);
        else if (playerPos.playerPosition == Position.Right)
            transform.position = new Vector3(rightX, transform.position.y, rightZ);
    }

    public void Jump()
    {
        if (charController.isGrounded)
        {
            AudioManager.instance.PlayJumpSound();
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
        AudioManager.instance.PlaySlideSound();
        GetComponent<PlayerAnimations>().PlaySlideAnimation();
        yield return new WaitForSecondsRealtime(2f);
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
