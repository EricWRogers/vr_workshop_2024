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

    public bool canTeleport = true;
    public float throwForceMultiplier = 1.5f;

    private XRBaseInteractor interactor;
    private Vector3 lastPosition;
    private Vector3 velocity;
    // Start is called before the first frame update

    void Start()
    {
        interactor = rightController.GetComponent<XRBaseInteractor>();
    }

    void FixedUpdate()
    {
        // Track velocity manually
        if (knifeSpawned && knife != null)
        {
            velocity = (rightController.transform.position - lastPosition) / Time.fixedDeltaTime;
            lastPosition = rightController.transform.position;
        }
    }
    public void SpawnAKnife()
    {
        if (canTeleport && !knifeSpawned)
        {
            knife = Instantiate(knifePrefab, rightController.transform.position, transform.rotation);
            bulletScript = knife.GetComponent<Bullet>();
#pragma warning disable CS0618
            interactor.StartManualInteraction(knife.GetComponent<XRGrabInteractable>());
#pragma warning restore CS0618

            knifeSpawned = true;
        }
    }
    /*public void SpawnAKnife()
    {
        if (canTeleport && !knifeSpawned)
        {
            if (!knifeSpawned)
            {
                knife = Instantiate(knifePrefab, rightController.transform.position, transform.rotation);
                //bulletScript = knife.GetComponent<Bullet>();
    #pragma    warning disable CS0618 // Type or member is obsolete, this line removes the error message
                rightController.GetComponent<XRBaseInteractor>().StartManualInteraction(knife.GetComponent<XRGrabInteractable>());
    #pragma    warning restore CS0618 // Type or member is obsolete, this line resumes error messages
                knifeSpawned = true;
            }
        }
    }*/
    public void ThrowKnife()
    {
        if (knife == null) return;

        interactor.EndManualInteraction();

        ThrowingKnife throwingKnife = knife.GetComponent<ThrowingKnife>();
        if (throwingKnife != null)
        {
            Vector3 throwDir = velocity.normalized;
            float throwStrength = velocity.magnitude * throwForceMultiplier;
            throwingKnife.Throw(throwDir, throwStrength);
            knife.GetComponent<Bullet>().enabled = true;
            knife.GetComponent<Bullet>().speed = 0;
        }

        knifeSpawned = false;
    }
    /*public void ThrowKnife()
    {
        rightController.GetComponent<XRBaseInteractor>().EndManualInteraction();
        knife.GetComponent<Bullet>().enabled = true;
        knifeSpawned = false;
    }*/

    public void TeleportTrue()
    {
        canTeleport = true;
    }
}