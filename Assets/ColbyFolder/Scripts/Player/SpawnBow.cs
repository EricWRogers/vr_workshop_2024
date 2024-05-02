using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnBow : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToSpawn;
    public GameObject leftController;
    public GameObject rightController;

    private GameObject bowReference;
    private bool bowSpawned = false;

    private void Start()
    {
        leftController = transform.GetChild(0).transform.GetChild(1).gameObject;
        rightController = transform.GetChild(0).transform.GetChild(2).gameObject;
    }

    [System.Obsolete] //This is because StartManualInteraction is deprecated, but it still works good for us at the moment and this line removes the error message
    public void SpawnBowNow()
    {
        if (!bowSpawned)
        {
            bowReference = Instantiate(objectToSpawn, leftController.transform.position, leftController.transform.rotation);
            leftController.GetComponent<XRBaseInteractor>().StartManualInteraction(bowReference.GetComponent<XRGrabInteractable>());
            bowSpawned = true;
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
