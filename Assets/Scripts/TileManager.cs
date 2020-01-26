using System.Collections;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public static TileManager instance;

    [Header("Platform Settings")]
    [SerializeField] int sizeOnScreenPlane = 0;

    [Header("Normal Platforms")]
    [SerializeField] private GameObject[] NormalPlatforms;

    [Header("Jump Platforms")] 
    [SerializeField] private GameObject[] JumpPlatforms;

    [Header("Slide Platforms")]
    [SerializeField] private GameObject[] SlidePlatforms;

    [Header("Coin Platforms")] 
    [SerializeField] private GameObject[] CoinPlatforms;

    [Header("Power Platforms")] 
    [SerializeField] private GameObject[] PowerPlatforms;

    [Header("Left Corners")] 
    [SerializeField] private GameObject[] LeftCorners;

    [Header("Right Corners")] 
    [SerializeField] private GameObject[] RightCorners;

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
                GameObject gl = Instantiate(LeftCorners[Random.Range(0,LeftCorners.Length)], lastPos, Quaternion.Euler(0, yRotation, 0));
                PlaneNumber++;
                SpawnLeftCorner();

            }
            else if (rand >= 1)
            {

                GameObject gr = Instantiate(RightCorners[Random.Range(0, RightCorners.Length)], lastPos, Quaternion.Euler(0, yRotation, 0));
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
        // 0 ile 100 arası rastgele bir sayı alacağız
        int randNum = Random.Range(0, 100);
        
        //%50 ihtimalle engelsiz platform
        if (randNum <= 50)
            go = Instantiate(NormalPlatforms[Random.Range(0, NormalPlatforms.Length)]);
        //%15 ihtimalle zıplatmalı platform 
        else if (randNum <= 65 && randNum > 50)
            go = Instantiate(JumpPlatforms[Random.Range(0, JumpPlatforms.Length)]);
        //%15 ihtimalle eğilmeli platform
        else if (randNum > 65 && randNum <= 80)
            go = Instantiate(SlidePlatforms[Random.Range(0, SlidePlatforms.Length)]);
        //%10 ihtimalle coinli platform
        else if (randNum > 80 && randNum <= 90)
            go = Instantiate(CoinPlatforms[Random.Range(0, CoinPlatforms.Length)]);
        //%10 ihtimalle güç veren platform
        else
            go = Instantiate(PowerPlatforms[Random.Range(0, CoinPlatforms.Length)]);

        CheckDirection(go);
    }
} //class
