using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public static TileManager instance;

    public GameObject[] Planes;
    public GameObject rotationLeft,rotationRight;

    private Vector3 lastPos;

    private bool moveForward, moveBack, moveRight, moveLeft;

    private float timer;

    private int planeNumber = 0 ;
    [SerializeField] int sizeOnScreenPlane;

    [SerializeField] int PlaneSize = 10;

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

        if (planeNumber > sizeOnScreenPlane)
        {
            CancelInvoke("SpawnTile");
        } else
        {
            SpawnTile();
        }
    }
    private void SpawnTile()
    {
            int randPlane = Random.Range(0, Planes.Length);
            GameObject tile = Instantiate(Planes[randPlane]);
            planeNumber++;
            tile.transform.position = lastPos;

        if (moveForward)
        {
            lastPos.z += PlaneSize;
            tile.transform.rotation = Quaternion.Euler(0, 0, 0);

            if (timer > Random.Range(5,25))
            {
                timer = 0;

                int rand = Random.Range(0, 2);

                if (rand < 1)
                {
                    GameObject gl = Instantiate(rotationLeft, lastPos, Quaternion.Euler(0,0,0));
                    planeNumber++;
                    moveLeft = true;
                    moveForward = false;
                    lastPos.x -= PlaneSize;
                  
                }
                else if (rand >= 1)
                {
                
                    GameObject gr =Instantiate(rotationRight, lastPos, Quaternion.Euler(0,0,0));
                    planeNumber++;
                    moveForward = false;
                    moveRight = true;
                    lastPos.x += PlaneSize;    
                }
            }

        }
        else if (moveRight)
        {
            lastPos.x += PlaneSize;
            tile.transform.rotation = Quaternion.Euler(0, 90, 0);

            if (timer > Random.Range(5, 25))
            {
                timer = 0;

                int rand = Random.Range(0, 2);

                if (rand < 1)
                {
                 
                    GameObject gl = Instantiate(rotationLeft, lastPos, Quaternion.Euler(0,90,0));
                    planeNumber++;
                    moveRight = false;
                    moveForward = true;
                    lastPos.z += PlaneSize;
              
                }
                else if (rand >= 1)
                {
                   
                    GameObject gr = Instantiate(rotationRight, lastPos, Quaternion.Euler(0,90,0));
                    planeNumber++;
                    moveBack = true;
                    moveRight = false;
                    lastPos.z -= PlaneSize;
                }
            }
        }

        else if (moveLeft)
        {
            
            lastPos.x -= PlaneSize;
            tile.transform.rotation = Quaternion.Euler(0, -90, 0);

            if (timer > Random.Range(5, 25))
            {
                timer = 0;

                int rand = Random.Range(0, 2);

                if (rand < 1)
                {
                 
                    GameObject gl =Instantiate(rotationLeft, lastPos, Quaternion.Euler(0,-90,0));
                    planeNumber++;
                    moveLeft = false;
                    moveBack = true;
                    lastPos.z -= PlaneSize;
                }
                else if (rand >= 1)
                {
              
                    GameObject gr =  Instantiate(rotationRight, lastPos, Quaternion.Euler(0,-90,0));
                    planeNumber++;
                    moveLeft = false;
                    moveForward = true;
                    lastPos.z += PlaneSize;
                }
            }
        }

        else if (moveBack)
        {
            
            lastPos.z -= PlaneSize;
            tile.transform.rotation = Quaternion.Euler(0, 180, 0);

            if (timer > Random.Range(5, 25))
            {
                timer = 0;

                int rand = Random.Range(0, 2);

                if (rand < 1)
                {
            
                    GameObject gl =  Instantiate(rotationLeft, lastPos, Quaternion.Euler(0,180,0));
                    planeNumber++;
                    moveRight = true;
                    moveBack = false ;
                    lastPos.x += PlaneSize;
                }
                else if (rand >= 1)
                {
            
                    GameObject gr = Instantiate(rotationRight, lastPos, Quaternion.Euler(0,180,0));
                    planeNumber++;
                    moveLeft = true;
                    moveBack = false;
                    lastPos.x -= PlaneSize;
                }
            }
        }
        

    


    }

    public int GetPlaneNumber
    {
        get { return planeNumber; }
        set { planeNumber = value; }
    }
} //class
