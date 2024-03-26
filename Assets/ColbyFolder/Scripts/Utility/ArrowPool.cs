using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    public static ArrowPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

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
            Rigidbody rigidbody = tmp.AddComponent<Rigidbody>();
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    // Whenever you have a reference to a pooled Object
    // all it takes to return it to the pool is to
    // set the gameObject back to inactive with pooledObject.SetActive(false);
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        Debug.Log("No pooled objects, don't forget to set any objects you are done with back to inactive to return them to the pool");
        return null;
    }
}
