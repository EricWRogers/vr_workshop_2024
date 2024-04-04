using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    public static ArrowPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    private Rigidbody arrRigidbody;
    private int index = 0;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            //get arrow rigidbody
            tmp.GetComponent<Rigidbody>().useGravity = false;
            tmp.GetComponent<Rigidbody>().isKinematic = true;
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    // Whenever you have a reference to a pooled Object
    // all it takes to return it to the pool is to
    // set the gameObject back to inactive with pooledObject.SetActive(false);
    public GameObject GetPooledObject()
    {
        GameObject arrow = pooledObjects[index];
        index++;
        if (index >= amountToPool)
        {
            index = 0;
        }
        return arrow;
    }
}
