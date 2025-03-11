using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class ArrowSwitchVR : MonoBehaviour
{
    static readonly Dictionary<string, InputFeatureUsage<bool>> availableButtons = new Dictionary<string, InputFeatureUsage<bool>>
    {
        {"triggerButton", CommonUsages.triggerButton },
    };

    public enum ButtonOption
    {
        triggerButton
    };

    [Header("Controller Settings")]
    public InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right;
    public InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left;
    public ButtonOption button = ButtonOption.triggerButton;

    [Header("Arrow Switching Events")]
    public UnityEvent OnNextArrow;
    public UnityEvent OnPreviousArrow;

    [Header("Arrow Types Reference")]
    public ArrowTypes arrowTypes; // Reference to ArrowTypes script

    private bool rightTriggerPressed = false;
    private bool leftTriggerPressed = false;

    void Start()
    {
        if (arrowTypes == null)
        {
            arrowTypes = FindObjectOfType<ArrowTypes>();
            if (arrowTypes == null)
            {
                Debug.LogError("ArrowTypes script not found!");
            }
        }
    }

    void Update()
    {
        CheckControllerInput(rightControllerCharacteristics, ref rightTriggerPressed, arrowTypes.SwitchToNextArrow);
        CheckControllerInput(leftControllerCharacteristics, ref leftTriggerPressed, arrowTypes.SwitchToPreviousArrow);
    }

    void CheckControllerInput(InputDeviceCharacteristics deviceCharacteristics, ref bool isPressed, UnityAction action)
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(deviceCharacteristics, devices);

        foreach (var device in devices)
        {
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out bool value) && value)
            {
                if (!isPressed)
                {
                    isPressed = true;
                    action.Invoke();
                }
            }
            else
            {
                isPressed = false;
            }
        }
    }
}
