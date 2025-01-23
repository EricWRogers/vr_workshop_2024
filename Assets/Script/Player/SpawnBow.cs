using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnBow : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToSpawn;
    [HideInInspector]
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

        Destroy(bowReference);
        bowSpawned = false;
    }
}
