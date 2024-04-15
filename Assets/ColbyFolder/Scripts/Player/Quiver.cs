using UnityEngine;

public class Quiver : MonoBehaviour
{
    private GameObject rightController;
    private SpawnArrowVR player;
    public bool canGrabArrow = false;

    private void Start()
    {
        rightController = GameObject.FindGameObjectWithTag("RightHand");
        player = FindObjectOfType<SpawnArrowVR>().GetComponent<SpawnArrowVR>();
    }

    public void DistanceCheck()
    {
        if (player != null)
        {
            if (canGrabArrow)
            {
                player.SpawnArrow();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == rightController.transform.GetChild(0).transform.GetChild(0).gameObject)
        {
            if (!player.arrowSpawned)
            {
                canGrabArrow = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == rightController.transform.GetChild(0).transform.GetChild(0).gameObject)
        {
            canGrabArrow = false;
        }
    }
}