using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class SpawnKnife : MonoBehaviour
{
    public GameObject knifePrefab;
    [HideInInspector]
    public GameObject knife;

    [HideInInspector]
    public GameObject rightController;
    [HideInInspector]
    public GameObject leftController;

    public bool knifeSpawned = false;
    // Start is called before the first frame update
    public void SpawnAKnife()
    {
        if (!knifeSpawned)
        {
            knife = Instantiate(knifePrefab, rightController.transform.position, rightController.transform.rotation);
#pragma warning disable CS0618 // Type or member is obsolete, this line removes the error message
            rightController.GetComponent<XRBaseInteractor>().StartManualInteraction(knife.GetComponent<XRGrabInteractable>());
#pragma warning restore CS0618 // Type or member is obsolete, this line resumes error messages
            knife.GetComponent<Arrow_v2>().trailEffect.SetActive(false);
            knifeSpawned = true;
        }
    }
}
