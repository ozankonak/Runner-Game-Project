using System.Collections;
using UnityEngine;

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
        StartCoroutine(StartDestroy(other));
    }
    IEnumerator StartDestroy(Collider other)
    {
        yield return new WaitForSecondsRealtime(2f);
        if (other.gameObject.tag == "Destroy")
        {
            other.transform.parent.gameObject.SetActive(false);
            TileManager.instance.PlaneNumber--;
        }
    }
}

