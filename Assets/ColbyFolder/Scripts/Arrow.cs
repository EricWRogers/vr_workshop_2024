using UnityEngine;

public class Arrow : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = FindObjectOfType<SpawnArrowVR>().gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Bow"))
        {
            player.GetComponent<SpawnArrowVR>().arrowNocked = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bow"))
        {
            player.GetComponent<SpawnArrowVR>().arrowNocked = false;
        }
    }
}