using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnBow : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToSpawn;
    private Vector3 attachPointPosition;
    private Quaternion attachPointRotation;
    [SerializeField]
    private GameObject leftController;

    private GameObject bowReference;
    private bool bowSpawned = false;

    private void Start()
    {
        attachPointPosition = objectToSpawn.transform.GetChild(5).position;
        attachPointRotation = objectToSpawn.transform.GetChild(5).rotation;
    }

    [System.Obsolete] //This is because StartManualInteraction is deprecated, but it still works good for us at the moment and this line removes the error message
    public void SpawnBowNow()
    {
        if (!bowSpawned)
        {
            //Vector3 spawnPosition = new Vector3(leftController.transform.position.x + attachPointPosition.x, leftController.transform.position.y + attachPointPosition.y, leftController.transform.position.z + attachPointPosition.z);
            //Vector3 eulerRotation = new Vector3(leftController.transform.rotation.x + attachPointRotation.x, leftController.transform.rotation.y + attachPointRotation.y, leftController.transform.rotation.z + attachPointRotation.z);
            //Quaternion spawnRotation = Quaternion.Euler(eulerRotation);
            //bowReference = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
            bowReference = Instantiate(objectToSpawn, leftController.transform.position, leftController.transform.rotation);
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
