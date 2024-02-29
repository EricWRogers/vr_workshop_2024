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

    public void SpawnBowNow()
    {
        if (!bowSpawned)
        {
            bowReference = Instantiate(objectToSpawn, leftController.transform.position, leftController.transform.rotation, leftController.transform);
            bowSpawned = true;

            //Logic to put the bow into the hand of the controller
            //leftController.GetComponent<XRController>().
        }
    }

    public void DestroyBow()
    {
        Destroy(bowReference);
        bowSpawned = false;
    }
}
