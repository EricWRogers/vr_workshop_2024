using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnArrowVR : MonoBehaviour
{
    public GameObject prefabarrow;
    [HideInInspector]
    public GameObject arrow;
    [SerializeField]
    private float thrust = 10f;
    [SerializeField]
    [Tooltip("n changes the logrithmic rolloff of the arrow thrust based on how far back it is pulled. Smaller numbers increase the weakening effect on low pull back amounts")]
    private float n = 0.1f;
    [HideInInspector]
    public GameObject rightController;
    [HideInInspector]
    public GameObject leftController;

    public bool arrowSpawned = false;
    public bool arrowNocked = false;
    public bool inFireZone = false;

    private float amountPulledBack = 1.0f;

    private void Start()
    {
        rightController = GameObject.FindGameObjectWithTag("RightHand");
        leftController = GameObject.FindGameObjectWithTag("LeftHand");
    }

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
        if (!arrowSpawned)
        {
            return;
        }
        if (!arrowNocked)
        {
            Destroy(arrow);
            arrowSpawned = false;
        }
        else
        {
            amountPulledBack = arrow.GetComponent<Arrow>().attachedObject.transform.parent.GetComponent<FollowTransformOnRail>().pullAmount;
            arrow.GetComponent<Arrow>().arrowAttached = false;
            arrow.GetComponent<Arrow>().attachedObject = null;
            arrow.GetComponent<Arrow>().hasBeenFired = true;
            arrow.GetComponent<Arrow>().trailEffect.SetActive(true);
            arrow.GetComponent<Rigidbody>().isKinematic = false;
            float arrowForce = ForceCalculator(amountPulledBack);
            arrow.GetComponent<Rigidbody>().AddForce(arrow.transform.forward * arrowForce, ForceMode.Impulse);
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

    private float ForceCalculator(float percentagePulledBack)
    {
        return ((n+1)*percentagePulledBack)/(n*amountPulledBack+1) * thrust;
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