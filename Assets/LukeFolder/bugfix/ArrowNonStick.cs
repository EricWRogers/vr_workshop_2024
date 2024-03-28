using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class ArrowNonStick : MonoBehaviour
{
    private GameObject player;
    public Collider sphereCollider;
    Rigidbody arrowRigidbody;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        arrowRigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Bow"))
        {
            player.GetComponent<SpawnArrowVR>().arrowNocked = true;
        }
        else if (other.CompareTag("NonStick"))
        {
            Debug.Log("Hit a nonstick object");
        }
        else if (!other.CompareTag("NonStick") && !other.CompareTag("Bow"))
        {
            arrowRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
        //Debug.Log(other);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bow"))
        {
            player.GetComponent<SpawnArrowVR>().arrowNocked = false;
        }
    }
}