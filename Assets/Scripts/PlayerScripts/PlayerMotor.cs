using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    private CharacterController charController;
    private Animator anim;
    private AudioSource source;

    private float speed = 20f;
    private float jumpSpeed = 10f;
    private float gravity = 20.0f;
    [SerializeField] AudioClip jumpClip;
    private Vector3 moveDirection = Vector3.zero;

    private float defaultHeight ;
    private float slideDistance;
    private bool canSlide = true;

    private bool moveForward,moveBack, moveRight, moveLeft;

    private bool middlePos, leftPos, rightPos;

    private Vector3 leftPoint, middlePoint, rightPoint;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        charController = GetComponent<CharacterController>();
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        defaultHeight = charController.height;
    //  slideDistance = charController.height * 0.5f + charController.radius; ;
        moveForward = true;
        middlePos = true;
    }

    private void Update()
    {
        Jump();
        Slide();
        SetRotation();
        Move();
        MoveRightAndLeft();
        
    }

    private void MoveRightAndLeft()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (middlePos)
            {
                middlePos = false;
                leftPos = true;
            }
            else if (leftPos)
            {
                leftPos = true;
            }
            else if (rightPos)
            {
                middlePos = true;
                rightPos = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (middlePos)
            {
                middlePos = false;
                rightPos = true;
            }
            else if (leftPos)
            {
                middlePos = true;
                leftPos = false;
            }
            else if (rightPos)
            {
                rightPos = true;
            }
        }
    }

    private void Move()
    {
        if (moveForward)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            charController.Move(Vector3.forward * speed * Time.deltaTime);
            if (middlePos) { transform.position = new Vector3(middlePoint.x, transform.position.y, transform.position.z); }
            else if (leftPos) { transform.position = new Vector3(leftPoint.x, transform.position.y, transform.position.z); }
            else if (rightPos) { transform.position = new Vector3(rightPoint.x, transform.position.y, transform.position.z); }
        }

        else if (moveRight)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            charController.Move(Vector3.right * speed * Time.deltaTime);
            if (middlePos) { transform.position = new Vector3(transform.position.x, transform.position.y, middlePoint.z); }
            else if (leftPos) { transform.position = new Vector3(transform.position.x, transform.position.y, leftPoint.z); }
            else if (rightPos) { transform.position = new Vector3(transform.position.x, transform.position.y, rightPoint.z); }
        }

        else if (moveLeft)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            charController.Move(Vector3.left * speed * Time.deltaTime);
            if (middlePos) { transform.position = new Vector3(transform.position.x, transform.position.y, middlePoint.z); }
            else if (leftPos) { transform.position = new Vector3(transform.position.x, transform.position.y, leftPoint.z); }
            else if (rightPos) { transform.position = new Vector3(transform.position.x, transform.position.y, rightPoint.z); }
        }
        else if (moveBack)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            charController.Move(Vector3.back * speed * Time.deltaTime);
            if (middlePos) { transform.position = new Vector3(middlePoint.x, transform.position.y, transform.position.z); }
            else if (leftPos) { transform.position = new Vector3(leftPoint.x, transform.position.y, transform.position.z); }
            else if (rightPos) { transform.position = new Vector3(rightPoint.x, transform.position.y, transform.position.z); }
        }
    }

    private void SetRotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (moveForward)
            {
                moveForward = false;
                moveLeft = true;

            }
            else if (moveBack)
            {
                moveRight = true;
                moveBack = false;
          

            } else if (moveRight)
            {
                moveForward = true;
                moveRight = false;
    
            } else if (moveLeft)
            {
                moveBack = true;
                moveLeft = false;

            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (moveForward)
            {
                moveForward = false;
                moveRight = true;
            }
            else if (moveBack)
            {
                moveLeft = true;
                moveBack = false;

            }
            else if (moveRight)
            {
                moveBack = true;
                moveRight = false;
            }
            else if (moveLeft)
            {
                moveForward = true;
                moveLeft = false;
            }
        }
    }

    private void Jump()
    {
        if (charController.isGrounded)
        {

            if (Input.GetKey(KeyCode.Space))
            {
                source.PlayOneShot(jumpClip);   
                anim.SetTrigger("Jump");    
                moveDirection.y = jumpSpeed;
            }
        } 
        
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        charController.Move(moveDirection * Time.deltaTime);
    }

    private void Slide()
    {

        if (charController.isGrounded)
        {
            if (Input.GetKey(KeyCode.C) && canSlide)
            {
                canSlide = false;
                charController.height = charController.height / 2f;
                charController.center = new Vector3(0f, charController.height / 2f, 0f);
                source.PlayOneShot(jumpClip);
                anim.SetTrigger("Slide");
                Invoke("GetUp", 1f);

            }
        }
    }
    void GetUp()
    {
        charController.height = defaultHeight;
        charController.center = new Vector3(0f, charController.height / 2f, 0f);
        canSlide = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LeftPoint")
        {

            leftPoint = other.gameObject.GetComponent<BoxCollider>().bounds.center;
        }
        if (other.gameObject.tag == "MiddlePoint")
        {
            middlePoint = other.gameObject.GetComponent<BoxCollider>().bounds.center;
        }
        if (other.gameObject.tag == "RightPoint")
        {
            rightPoint = other.gameObject.GetComponent<BoxCollider>().bounds.center;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Destroy")
        {
            Destroy(other.gameObject.transform.parent.gameObject, 2f);
            TileManager.instance.GetPlaneNumber--;
        }
    }

}

