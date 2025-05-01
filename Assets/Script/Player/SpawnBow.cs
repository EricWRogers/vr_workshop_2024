using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnBow : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToSpawn;
    [HideInInspector]

    [SerializeField]
    private GameObject indicatorPrefab;
    public GameObject leftController;
    [HideInInspector]
    public GameObject rightController;

    private GameObject bowReference;
    private bool bowSpawned = false;
    private BowString bowString;

    [System.Obsolete] //This is because StartManualInteraction is deprecated, but it still works good for us at the moment and this line removes the error message
    public void SpawnBowNow()
    {
        if (!bowSpawned)
        {
            bowReference = Instantiate(objectToSpawn, leftController.transform.position, leftController.transform.rotation);
            leftController.GetComponent<XRBaseInteractor>().StartManualInteraction(bowReference.GetComponent<XRGrabInteractable>());
            bowString = bowReference.transform.GetChild(0).GetComponent<BowString>();
            bowSpawned = true;
            bowString.leftController = leftController;
            bowString.rightController = rightController;
        }
    }

    private void SpawnIndicator()
    {
        if (bowReference == null || indicatorPrefab == null) return;

        // Create indicator and parent it to the bow
        GameObject indicator = Instantiate(indicatorPrefab, bowReference.transform);
        indicator.transform.localPosition = new Vector3(0.2f, 0, 0); // Adjust placement
        indicator.transform.localRotation = Quaternion.identity;

        // Ensure the indicator updates with arrow type

    }

    public void DestroyBow()
    {
        SpawnArrowVR player = leftController.transform.parent.transform.parent.GetComponent<SpawnArrowVR>();
        if (player.arrowNocked)
        {
            player.arrowNocked = false;
            Destroy(player.arrow);
            player.arrowSpawned = false;
            rightController.GetComponent<XRBaseInteractor>().EndManualInteraction();
        }

        if (bowReference != null)
        {
        Destroy(bowReference);
        }
        
        bowSpawned = false;
    }
}
