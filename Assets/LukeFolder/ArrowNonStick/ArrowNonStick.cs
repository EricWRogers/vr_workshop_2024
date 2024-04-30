using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class ArrowNonStick : MonoBehaviour
{
    private GameObject player;
    public Collider sphereCollider;
    public Collider boxCollider;
    public Rigidbody arrowRigidbody;
    bool firstContact;
    public Arrow arrowScript;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        arrowRigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) // make the arrow sticking a sphere cast in fixed update, will make it more reliable
    {
        /* if (!other.CompareTag("NonStick") && !other.CompareTag("Bow"))
         {
             arrowRigidbody.constraints = RigidbodyConstraints.FreezeAll;
         }
         if (other.CompareTag("Bow"))
         {
             player.GetComponent<SpawnArrowVR>().arrowNocked = true;
         }
         else if (other.CompareTag("NonStick"))
         {
             Debug.Log("Hit a nonstick object");
         }

         //Debug.Log(other);*/
    }

    private void FixedUpdate()
    {
        if (!firstContact)
        {
            RaycastHit hit;
            if (Physics.Raycast(sphereCollider.transform.position, sphereCollider.transform.forward, out hit, 1.0f)) //, 0, QueryTriggerInteraction.Ignore  the integer between the float and querytriggerinteraction is the layer number, the default layer. it will ignore any other.
            {
                Debug.Log(hit.collider.gameObject);
                if ((!hit.collider.gameObject.CompareTag("NonStick") || !hit.collider.gameObject.CompareTag("Bow") || !hit.collider.gameObject.CompareTag("FireZone")) && arrowScript.arrowNocked == false)
                {
                    firstContact = true;
                    //arrowRigidbody.velocity = Vector3.zero;
                    transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    arrowRigidbody.constraints = RigidbodyConstraints.FreezeAll;
                    //boxCollider.enabled = false; //error! the targets check for the box collider to trigger them!

                }
                if (hit.collider.gameObject.CompareTag("NonStick"))
                {
                    //Debug.Log("Hit nonstick");
                }
            }

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