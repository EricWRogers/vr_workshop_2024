using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPillarRotation : MonoBehaviour
{
    public List<GameObject> objectsToRotate;

    void Start()
    {
        RotateObjects();
    }

    void RotateObjects()
    {
        foreach (GameObject obj in objectsToRotate)
        {
            // Generate a random rotation angle around the Y-axis
            float randomAngle = Random.Range(0f, 360f);

            // Apply the rotation to the object
            obj.transform.rotation = Quaternion.Euler(0f, randomAngle, 0f);
        }
    }
}
