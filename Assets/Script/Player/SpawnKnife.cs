using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using SuperPupSystems.Helper;

public class SpawnKnife : MonoBehaviour
{
    public GameObject knifePrefab;
    [HideInInspector]
    public GameObject knife;
    private Bullet bulletScript;

    [HideInInspector]
    public GameObject rightController;
    [HideInInspector]
    public GameObject leftController;

    public bool knifeSpawned = false;
    private ThrowingKnife TKScript;

    public bool canTeleport = true;
    // Start is called before the first frame update
    public void SpawnAKnife()
    {
        if (canTeleport)
        {
            if (!knifeSpawned)
            {
                knife = Instantiate(knifePrefab, rightController.transform.position, transform.rotation);
                //bulletScript = knife.GetComponent<Bullet>();
    #pragma    warning disable CS0618 // Type or member is obsolete, this line removes the error message
                rightController.GetComponent<XRBaseInteractor>().StartManualInteraction(knife.GetComponent<XRGrabInteractable>());
    #pragma    warning restore CS0618 // Type or member is obsolete, this line resumes error messages
                knifeSpawned = true;
                TKScript = knife.GetComponent<ThrowingKnife>();
            }
        }
    }
    public void ThrowKnife()
    {
        rightController.GetComponent<XRBaseInteractor>().EndManualInteraction();

        float mag = Vector3.Magnitude(TKScript.curPos - TKScript.lastPos);
        Vector3 direction = (TKScript.curPos - TKScript.lastPos).normalized;

        knife.transform.rotation = Quaternion.LookRotation(direction);
        knife.GetComponent<Bullet>().speed = mag * 30;
        knife.GetComponent<Bullet>().enabled = true;
        knifeSpawned = false;
    }

    public void TeleportTrue()
    {
        canTeleport = true;
    }
}