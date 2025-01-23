using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnArrowVRNonStick : MonoBehaviour
{
    public GameObject prefabarrow;
    private GameObject arrow;
    public float thrust = 5.0f;
    public GameObject rightController;

    private bool arrowSpawned = false;
    public bool arrowNocked = false;

    public void SpawnArrow()
    {
        if (!arrowSpawned)
        {
            arrow = Instantiate(prefabarrow, rightController.transform.position, rightController.transform.rotation);
            #pragma warning disable CS0618 // Type or member is obsolete, this line removes the error message
            rightController.GetComponent<XRBaseInteractor>().StartManualInteraction(arrow.GetComponent<XRGrabInteractable>());
            #pragma warning restore CS0618 // Type or member is obsolete, this line resumes error messages
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
            arrowSpawned = false;
            arrowNocked = false;
        }
    }

    /*  put trigger on end of arrow, (done)
        on trigger enter, check what its entering?
        if wall thats "sticky" (aka check for non stick tag), then calculate forward angle of arrow and push it into the wall a bit
        freeze physics to ensure it doesnt move
    */

}