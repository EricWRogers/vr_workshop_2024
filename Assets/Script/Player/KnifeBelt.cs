using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBelt : MonoBehaviour
{
    [HideInInspector]
    public GameObject rightController;
    [HideInInspector]
    public GameObject leftController;
    private SpawnKnife spawnKnife;
    public bool canGrabKnife = false;

    private void Start()
    {
        spawnKnife = FindObjectOfType<SpawnKnife>().GetComponent<SpawnKnife>();
    }

    public void DistanceCheck()
    {
        if (spawnKnife != null)
        {
            if (canGrabKnife)
            {
                spawnKnife.SpawnAKnife();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (rightController.transform.childCount > 0 && rightController.transform.GetChild(0).childCount > 0)
        {
            if (other.gameObject == rightController.transform.GetChild(0).transform.GetChild(0).gameObject)
            {
                if (!spawnKnife.knifeSpawned)
                {
                    canGrabKnife = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == rightController.transform.GetChild(0).transform.GetChild(0).gameObject)
        {
            canGrabKnife = false;
        }
    }
}
