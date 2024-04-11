using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnArrowVR : MonoBehaviour
{
    public GameObject prefabarrow;
    private GameObject arrow;
    public float thrust = 5.0f;
    public GameObject rightController;

    private bool arrowSpawned = false;
    public bool arrowNocked = false;
    public bool inFireZone = false;

    //Aiden Added This
    private float velocityThreshold = 0.01f;

    public void SpawnArrow()
    {
        if (!arrowSpawned)
        {
            arrow = Instantiate(prefabarrow, rightController.transform.position, rightController.transform.rotation);
            #pragma warning disable CS0618 // Type or member is obsolete, this line removes the error message
            rightController.GetComponent<XRBaseInteractor>().StartManualInteraction(arrow.GetComponent<XRGrabInteractable>());
            #pragma warning restore CS0618 // Type or member is obsolete, this line resumes error messages
            if (inFireZone)
            {
                arrow.GetComponent<Arrow>().onFire = true;
            }
            arrowSpawned = true;
        }
    }

    public void ReleaseArrow()
    {
        if (!arrowNocked)
        {
            Destroy(arrow);
            arrowSpawned = false;
        }
        else
        {
            rightController.GetComponent<XRBaseInteractor>().EndManualInteraction();
            arrow.transform.parent = null;
            arrow.GetComponent<Rigidbody>().isKinematic = false;
            arrow.GetComponent<Rigidbody>().AddForce(arrow.transform.forward * thrust, ForceMode.Impulse);
            if (inFireZone)
            {
                arrow.GetComponent<Arrow>().onFire = true;
                arrow.GetComponent<Arrow>().fireTimer = arrow.GetComponent<Arrow>().lengthOfFire;
            }
            arrowSpawned = false;
            arrowNocked = false;
            //Aiden Added This
            arrow.GetComponent<Arrow>().hasBeenFired = true;//

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FireZone"))
        {
            inFireZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FireZone"))
        {
            inFireZone = false;
        }
    }

    //Aiden Added This
    private void Update()
    {
        // Check if arrow is spawned and it's not moving (velocity close to zero)
        if (arrowSpawned && arrow != null && arrow.GetComponent<Rigidbody>().velocity.magnitude < velocityThreshold)
        {
            arrow.GetComponent<Arrow>().hasBeenFired = false;
        }

        if(arrow.GetComponent<Arrow>().hasBeenFired == true)
            {
                arrow.GetComponent<Arrow>().trailEffect.SetActive(true);
            }else
            {
                arrow.GetComponent<Arrow>().trailEffect.SetActive(false);
            }
    }//
}