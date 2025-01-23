using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRope : MonoBehaviour
{
    public GameObject topSection;
    public GameObject bottomSection;

    public float yAxis = 6.0f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("AidensArrow"))
        {
            Debug.Log("You have it the Trigger Collider");
            Instantiate(topSection, new Vector3(0, 10, 0), Quaternion.identity);
            Instantiate(bottomSection, new Vector3(0, yAxis, 0), Quaternion.identity);
        }
    }
}
