using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MenuScriptVR : MonoBehaviour
{
    public GameObject canvasPrefabObject;
    Camera gameCamera;
    //bool keyDown = false; //for the keypress toggle for the menu
    public float menuDistance = 9.0f; //meant to push the z value of the menu away from camera

    void Awake()
    {
        gameCamera = Camera.main;

        canvasPrefabObject.transform.LookAt(gameCamera.transform, Vector3.up);
        canvasPrefabObject.transform.Rotate(0, 180, 0);
        canvasPrefabObject.transform.position =
        (gameCamera.transform.position + gameCamera.transform.forward * menuDistance);
    }

    void Update()
    {

        if (Input.GetKeyDown("`"))
        {
            OpenMenu();
        }
    }
    public void OpenMenu()
    {
        if (canvasPrefabObject.activeSelf)
        {
            canvasPrefabObject.SetActive(false);
            return;
        }
        else if (!canvasPrefabObject.activeSelf)
        {
            canvasPrefabObject.SetActive(true);
            canvasPrefabObject.transform.LookAt(gameCamera.transform, Vector3.up);
            canvasPrefabObject.transform.Rotate(0, 180, 0);
            canvasPrefabObject.transform.position =
            (gameCamera.transform.position + gameCamera.transform.forward * menuDistance);
            return;
        }

    }
}
