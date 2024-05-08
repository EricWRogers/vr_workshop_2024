using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BowString : MonoBehaviour
{
    [HideInInspector]
    public GameObject rightController;
    [HideInInspector]
    public GameObject leftController;
    private SpawnArrowVR player;
    public Vector3 localPosition;
    public FollowTransformOnRail joint;

    private void Start()
    {
        localPosition = transform.localPosition;
        //rightController = GameObject.FindGameObjectWithTag("RightHand");
        //leftController = GameObject.FindGameObjectWithTag("LeftHand");
        player = FindObjectOfType<SpawnArrowVR>().GetComponent<SpawnArrowVR>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the right controller touches the bowstring
        if (other.gameObject == rightController.transform.GetChild(0).transform.GetChild(0).gameObject)
        {
            if (player.arrowSpawned)
            {
                player.arrowNocked = true;
                rightController.GetComponent<XRBaseInteractor>().EndManualInteraction();
                player.arrow.GetComponent<XRGrabInteractable>().throwOnDetach = false;
                player.arrow.transform.parent = null;
                //Attaches based on an attachPoint on the joint
                player.arrow.GetComponent<Arrow>().Attach(joint.transform.GetChild(1).gameObject);
                OnGrab();
            }
        }
    }

    public void OnGrab()
    {
        if (player.arrowSpawned)
        {
            #pragma warning disable CS0618 // Type or member is obsolete, this line removes the error message
            rightController.GetComponent<XRBaseInteractor>().StartManualInteraction(GetComponent<XRGrabInteractable>());
            #pragma warning restore CS0618 // Type or member is obsolete, this line resumes error messages
        }
        joint.isGrabbing = true;
    }

    public void OnStopGrab()
    {
        if (player.arrowSpawned)
        {
            rightController.GetComponent<XRBaseInteractor>().EndManualInteraction();
        }
        transform.localPosition = localPosition;
        joint.isGrabbing = false;
    }
}