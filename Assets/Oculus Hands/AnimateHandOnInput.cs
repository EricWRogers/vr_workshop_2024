using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;

    void Update()
    {
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        GetComponent<Animator>().SetFloat("Trigger", triggerValue);

        float gripValue = gripAnimationAction.action.ReadValue<float>();
        GetComponent<Animator>().SetFloat("Grip", gripValue);
    }
}
