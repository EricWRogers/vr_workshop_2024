using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnArrowVR : MonoBehaviour
{
    public GameObject prefabarrow;
    [HideInInspector]
    public GameObject arrow;
    public float thrust = 5.0f;
    public GameObject rightController;

    public bool arrowSpawned = false;
    public bool arrowNocked = false;
    public bool inFireZone = false;

    public void SpawnArrow()
    {
        if (!arrowSpawned)
        {
            arrow = Instantiate(prefabarrow, rightController.transform.position, rightController.transform.rotation);
            #pragma warning disable CS0618 // Type or member is obsolete, this line removes the error message
            rightController.GetComponent<XRBaseInteractor>().StartManualInteraction(arrow.GetComponent<XRGrabInteractable>());
            #pragma warning restore CS0618 // Type or member is obsolete, this line resumes error messages
            arrow.GetComponent<Arrow>().trailEffect.SetActive(false);
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
            arrow.GetComponent<Arrow>().arrowAttached = false;
            arrow.GetComponent<Arrow>().attachedObject = null;
            arrow.GetComponent<Arrow>().hasBeenFired = true;
            arrow.GetComponent<Arrow>().trailEffect.SetActive(true);
            arrow.GetComponent<Rigidbody>().isKinematic = false;
            arrow.GetComponent<Rigidbody>().AddForce(arrow.transform.forward * thrust, ForceMode.Impulse);
            if (inFireZone)
            {
                arrow.GetComponent<Arrow>().onFire = true;
                arrow.GetComponent<Arrow>().fireTimer = arrow.GetComponent<Arrow>().lengthOfFire;
            }
            arrowSpawned = false;
            arrowNocked = false;
            rightController.GetComponent<XRBaseInteractor>().EndManualInteraction();
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
}