using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ArrowSwitch : MonoBehaviour
{
    public ArrowTypes arrowTypesScript; // Reference to ArrowTypes script
    public Transform wristIndicator; //  CHANGE THIS TO YOUR FINAL WRIST OBJECT NAME 

    private InputDevice rightController;
    private bool triggerPressed = false;
    private Vector2 joystickInput;
    private Quaternion initialRotation;

    void Start()
    {
        return;
        InitializeRightController();

        if (wristIndicator != null)
        {
            initialRotation = wristIndicator.localRotation; // Store initial wrist rotation
        }
        else
        {
            Debug.LogError("Wrist Indicator not assigned!"); // Warn if not set
        }
    }

    void Update()
    {
        /*if (!rightController.isValid)
        {
            InitializeRightController(); // Reinitialize if controller disconnects
            return;
        }

        HandleInput();*/
    }

    void InitializeRightController()
    {
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

    void HandleInput()
    {
        bool triggerButtonValue = false;

        if (rightController.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonValue) && triggerButtonValue)
        {
            rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystickInput);

            if (joystickInput.x > 0.5f && !triggerPressed) // Right on joystick
            {
                triggerPressed = true;
                SwitchArrowType(1); // Next arrow type
                RotateWristIndicator(30f); // Rotate UI Right
            }
            else if (joystickInput.x < -0.5f && !triggerPressed) // Left on joystick
            {
                triggerPressed = true;
                SwitchArrowType(-1); // Previous arrow type
                RotateWristIndicator(-30f); // Rotate UI Left
            }
        }
        else
        {
            triggerPressed = false;
        }
    }

    public void LeftHandleInput()
    {
            
        SwitchArrowType(-1); // Previous arrow type
        //RotateWristIndicator(-30f); // Rotate UI Left
            
    }

        public  void RightHandleInput()
    {
            
        SwitchArrowType(1); // Next arrow type
        //RotateWristIndicator(-30f); // Rotate UI Right
            
    }



    void SwitchArrowType(int direction)
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

    void RotateWristIndicator(float rotationAmount)
    {
        if (wristIndicator != null)
        {
            wristIndicator.localRotation *= Quaternion.Euler(0, rotationAmount, 0);
        }
    }
}
