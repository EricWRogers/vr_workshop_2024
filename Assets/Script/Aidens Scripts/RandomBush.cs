using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBush : MonoBehaviour
{
    public List<GameObject> objectsToTransform;

    public float minScale = 0.45f;
    public float maxScale = 0.6f;

    void Start()
    {
        TransformObjects();
    }

    void TransformObjects()
    {
        foreach (GameObject obj in objectsToTransform)
        {
            float randomAngleZ = Random.Range(0f, 360f);

            // Apply random rotation to the object
            obj.transform.rotation = Quaternion.Euler(-90f, 0f, randomAngleZ);

            // Generate random scale factor between minScale and maxScale
            float randomScale = Random.Range(minScale, maxScale);

            // Apply random scale to the object
            obj.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        }
    }

}
