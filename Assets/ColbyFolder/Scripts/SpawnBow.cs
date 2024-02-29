using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnBow : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToSpawn;
    [SerializeField]
    private GameObject leftController;

    private GameObject bowReference;
    private bool bowSpawned = false;

    [System.Obsolete] //This is because StartManualInteraction is deprecated, but it still works good for us at the moment and this line removes the error message
    public void SpawnBowNow()
    {
        if (!bowSpawned)
        {
            bowReference = Instantiate(objectToSpawn, leftController.transform.position, leftController.transform.rotation, leftController.transform); 
            leftController.GetComponent<XRBaseInteractor>().StartManualInteraction(bowReference.GetComponent<XRGrabInteractable>());
            bowSpawned = true;
        }
    }

    public void DestroyBow()
    {
        Destroy(bowReference);
        bowSpawned = false;
    }
}
