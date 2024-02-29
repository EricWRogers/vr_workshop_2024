using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnArrowVR : MonoBehaviour
{
    public GameObject prefabarrow;
    Rigidbody arr_rigidbody;
    private GameObject arrow;
    public float thrust = 5.0f;
    public GameObject rightController;

    private bool arrowSpawned = false;
    public bool arrowNocked = false;

    void Start()
    {
        arr_rigidbody = prefabarrow.GetComponent<Rigidbody>();
    }

    //[System.Obsolete] //This is because StartManualInteraction is deprecated, but it still works good for us at the moment and this line removes the error message
    public void SpawnArrow()
    {
        if (!arrowSpawned)
        {
            arrow = Instantiate(prefabarrow, rightController.transform.position, rightController.transform.rotation, rightController.transform);
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
            arrow.transform.parent = null;
            arrow.GetComponent<Rigidbody>().isKinematic = false;
            arr_rigidbody.AddForce(arrow.transform.forward * thrust, ForceMode.Impulse);
            arrowSpawned = false;
        }
    }
}