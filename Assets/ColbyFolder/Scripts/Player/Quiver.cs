using UnityEngine;

public class Quiver : MonoBehaviour
{
    [Tooltip("This value determines how far from the quiver the hand must be to grab an arrow. Smaller values mean closer")]
    public double distance;
    public GameObject distanceCheckPoint;
    private GameObject rightController;
    private SpawnArrowVR player;

    private void Start()
    {
        rightController = GameObject.FindGameObjectWithTag("RightHand");
        player = FindObjectOfType<SpawnArrowVR>().GetComponent<SpawnArrowVR>();
    }

    public void DistanceCheck()
    {
        if (player != null)
        {
            if (Vector3.Distance(rightController.transform.position, distanceCheckPoint.transform.position) <= distance)
            {
                player.SpawnArrow();
            }
        }
    }
}