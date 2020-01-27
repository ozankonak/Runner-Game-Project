using System.Collections.Generic;
using UnityEngine;

public class LeftCornerPooler : MonoBehaviour
{
    public static LeftCornerPooler instance;

    private List<GameObject> pooledObjects;

    [SerializeField] private GameObject[] objectToPools;
    [SerializeField] private int amountToPool;

    private bool shouldExpand = true;

    private int averageOfObjects;

    private void Awake()
    {
        instance = this;
        pooledObjects = new List<GameObject>();
    }

    private void Start()
    {
        AddObjectsToPooler();
    }

    private void AddObjectsToPooler()
    {
        averageOfObjects = amountToPool / objectToPools.Length;

        for (int i = 0; i < averageOfObjects; i++)
        {
            for (int j = 0; j < objectToPools.Length; j++)
            {
                GameObject obj = (GameObject)Instantiate(objectToPools[j]);
                obj.SetActive(false);
                obj.transform.SetParent(this.transform);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
                return pooledObjects[i];
        }

        if (shouldExpand)
        {
            int rand = Random.Range(0, objectToPools.Length);
            GameObject obj = (GameObject)Instantiate(objectToPools[rand]);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            return obj;
        }
        else
        {
            return null;
        }
    }
}
