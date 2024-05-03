using UnityEngine;

public class Quiver : MonoBehaviour
{
    [HideInInspector]
    public GameObject rightController;
    [HideInInspector]
    public GameObject leftController;
    private SpawnArrowVR player;
    public bool canGrabArrow = false;

    private void Start()
    {
        rightController = GameObject.FindGameObjectWithTag("RightHand");
        leftController = GameObject.FindGameObjectWithTag("LeftHand");
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
        if (rightController.transform.childCount > 0 && rightController.transform.GetChild(0).childCount > 0)
        {
            if (other.gameObject == rightController.transform.GetChild(0).transform.GetChild(0).gameObject)
            {
                if (!player.arrowSpawned)
                {
                    canGrabArrow = true;
                }
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