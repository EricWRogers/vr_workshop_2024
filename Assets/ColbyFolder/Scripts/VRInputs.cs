using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRInputs : XRBaseInteractable
{
    [SerializeField]
    private GameObject grabbableObject;

    [SerializeField]
    private Transform transformToInstantiate;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Instantiate object
        GameObject newObject = Instantiate(grabbableObject, transformToInstantiate.position, Quaternion.identity, transformToInstantiate);

        // Get grab interactable from prefab
        XRGrabInteractable objectInteractable = newObject.GetComponent<XRGrabInteractable>();

        // Select object into same interactor
        interactionManager.SelectEnter(args.interactorObject, objectInteractable);

        base.OnSelectEntered(args);
    }
}