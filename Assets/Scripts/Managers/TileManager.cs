using System.Collections;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;

    [Header("Platform Settings")]
    [SerializeField] int sizeOnScreenPlane = 0;

    private int PlaneSize = 10; //Platformun boyutu 
    private Vector3 lastPos;
    private bool moveForward, moveBack, moveRight, moveLeft;
    private float minTimeForSpawnCorner = 5f;
    private float maxTimeForSpawnCorner = 25f;
    private float timer;
    public int PlaneNumber { get; set; } = 0;



    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        lastPos = Vector3.zero;
        moveForward = true;

        InvokeRepeating("SpawnTile", 0.1f, 0.1f);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (PlaneNumber > sizeOnScreenPlane)
            CancelInvoke("SpawnTile");
        else
            SpawnTile();
    }


    private void SpawnTile()
    {
        GameObject tile = null;
        CreateCombinations(tile);
    }

    private void CheckDirection(GameObject go)
    {
        PlaneNumber++;
        go.transform.position = lastPos;

        if (moveForward)
        {
            lastPos.z += PlaneSize;
            CheckLeftOrRight(go, 0);
        }
        else if (moveRight)
        {
            lastPos.x += PlaneSize;
            CheckLeftOrRight(go, 90);
        }
        else if (moveLeft)
        {
            lastPos.x -= PlaneSize;
            CheckLeftOrRight(go, -90);
        }
        else if (moveBack)
        {
            lastPos.z -= PlaneSize;
            CheckLeftOrRight(go, 180);
        }
    }

    private void CheckLeftOrRight(GameObject go,float yRotation)
    {
        go.transform.rotation = Quaternion.Euler(0, yRotation, 0);

        if (timer > Random.Range(minTimeForSpawnCorner, maxTimeForSpawnCorner))
        {
            timer = 0;

            int rand = Random.Range(0, 2);

            if (rand < 1)
            {
                GameObject gl = Instantiate(LeftCornerPooler.instance.GetPooledObject(), lastPos, Quaternion.Euler(0, yRotation, 0));
                if (gl != null)
                    gl.SetActive(true);
                PlaneNumber++;
                SpawnLeftCorner();

            }
            else if (rand >= 1)
            {

                GameObject gr = Instantiate(RightCornerPooler.instance.GetPooledObject(), lastPos, Quaternion.Euler(0, yRotation, 0));
                if (gr != null)
                    gr.SetActive(true);
                PlaneNumber++;
                SpawnRightCorner();
            }
        }
    }

    private void SpawnLeftCorner()
    {
        if (moveForward)
        {
            moveForward = false;
            moveLeft = true;
            lastPos.x -= PlaneSize;
        }
        else if (moveRight)
        {
            moveRight = false;
            moveForward = true;
            lastPos.z += PlaneSize;
        }
        else if (moveLeft)
        {
            moveLeft = false;
            moveBack = true;
            lastPos.z -= PlaneSize;
        }
        else if (moveBack)
        {
            moveBack = false;
            moveRight = true;
            lastPos.x += PlaneSize;
        }
    }

    private void SpawnRightCorner()
    {
        if (moveForward)
        {
            moveForward = false;
            moveRight = true;
            lastPos.x += PlaneSize;
        }
        else if (moveRight)
        {
            moveRight = false;
            moveBack = true;
            lastPos.z -= PlaneSize;
        }
        else if (moveLeft)
        {
            moveLeft = false;
            moveForward = true;
            lastPos.z += PlaneSize;
        }
        else if (moveBack)
        {
            moveBack = false;
            moveLeft = true;
            lastPos.x -= PlaneSize;
        }
    }

    private void CreateCombinations(GameObject go)
    {
        int randNum = Random.Range(0, 100); // - Random number between 0 and 100
        
        if (randNum <= 50)
            go = NormalPooler.instance.GetPooledObject(); // - %50 chance to normal platform
            else if (randNum <= 65 && randNum > 50)
            go = JumpPlatformPooler.instance.GetPooledObject(); // - %15 chance to jump platform
        else if (randNum > 65 && randNum <= 80)
            go = SlidePlatformPooler.instance.GetPooledObject(); // - %15 chance to slide platform
        else if (randNum > 80 && randNum <= 90)
            go = CoinPlatformPooler.instance.GetPooledObject(); // - %10 chance to coin platform
        else
            go = PowerPlatformPooler.instance.GetPooledObject(); // - %50 chance to power platform


        if (go != null)
            go.SetActive(true);

        CheckDirection(go);
    }
} //class
