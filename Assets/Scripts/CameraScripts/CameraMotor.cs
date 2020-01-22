using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {

    private Transform playerPos;
    private Vector3 OffSet;
    private Vector3 moveVector;

    //FOR ANIMATION
    private float transition = 0.0f;
    private float animationDuration = 3.0f;
    private Vector3 animationOffset = new Vector3(0, 5, -5);

    private void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        OffSet = transform.position - playerPos.position;
    }
    private void Update()
    {
        moveVector= playerPos.position + OffSet;

        //X
        moveVector.x = 0;
        //Y
        moveVector.y = Mathf.Clamp(moveVector.y, 2, 5);

        //Z

        if (transition > 1.0f )
        {
            transform.position = moveVector;
        }else
        {
            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
            transition += Time.deltaTime * 1 / animationDuration;
            transform.LookAt(playerPos.position + Vector3.up);
        }

       
    }




}
