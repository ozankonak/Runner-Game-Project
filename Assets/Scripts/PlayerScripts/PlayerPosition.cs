using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public enum Position { Left, Middle, Right}

public class PlayerPosition : MonoBehaviour
{
    [HideInInspector]
    public Position playerPosition = Position.Middle;

    public Vector3 LeftPoint { get; private set; }
    public Vector3 MiddlePoint { get; private set; }
    public Vector3 RightPoint { get; private set; }

    public void MoveLeft()
    {
        if (playerPosition == Position.Middle)
                playerPosition = Position.Left;
        else if (playerPosition == Position.Left)
                playerPosition = Position.Left;
        else if (playerPosition == Position.Right)
                playerPosition = Position.Middle;
    }

    public void MoveRight()
    {
        if (playerPosition == Position.Middle)
                playerPosition = Position.Right;
        else if (playerPosition == Position.Left)
                playerPosition = Position.Middle;
        else if (playerPosition == Position.Right)
                playerPosition = Position.Right;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LeftPoint")
        {

            LeftPoint = other.gameObject.GetComponent<BoxCollider>().bounds.center;
        }
        if (other.gameObject.tag == "MiddlePoint")
        {
            MiddlePoint = other.gameObject.GetComponent<BoxCollider>().bounds.center;
        }
        if (other.gameObject.tag == "RightPoint")
        {
            RightPoint = other.gameObject.GetComponent<BoxCollider>().bounds.center;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Destroy")
        {
            Destroy(other.gameObject.transform.parent.gameObject, 2f);
            TileManager.instance.PlaneNumber--;
        }
    }

}

