using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ArrowSwitch : MonoBehaviour
{
    public ArrowTypes arrowTypesScript;
    private InputDevice rightController;
    private bool triggerPressed = false;

    void Start()
    {
        // Get the right-hand controller
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, devices);

        if (devices.Count > 0)
        {
            rightController = devices[0];
        }
        else
        {
            Debug.LogError("Right controller not found!");
        }
    }

    void Update()
    {
        if (rightController.isValid)
        {
            bool triggerButtonValue = false;

            // Check if the right grip button is pressed
            if (rightController.TryGetFeatureValue(CommonUsages.gripButton, out triggerButtonValue) && triggerButtonValue)
            {
                if (!triggerPressed)
                {
                    triggerPressed = true;
                    SwitchArrowType();
                }
            }
            else
            {
                triggerPressed = false;
            }
        }
    }

    void SwitchArrowType()
    {
        if (arrowTypesScript != null)
        {
            arrowTypesScript.SwitchArrowType();
            Debug.Log("Switched to: " + arrowTypesScript.typesOfArrow);
        }
        else
        {
            Debug.LogError("ArrowTypes script not assigned!");
        }
    }
}
