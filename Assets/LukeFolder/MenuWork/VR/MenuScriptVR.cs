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
    public GameObject[] handObjects;
    public GameObject postProcessingVolume;

    void Awake()
    {
        gameCamera = Camera.main;

        canvasPrefabObject.transform.LookAt(gameCamera.transform, Vector3.up);
        canvasPrefabObject.transform.Rotate(0, 180, 0);
        canvasPrefabObject.transform.position =
        (gameCamera.transform.position + gameCamera.transform.forward * menuDistance);
        
    }

    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetKeyDown("`"))
        {
            OpenMenu();
            handObjects = GameObject.FindGameObjectsWithTag("Hands");
        }
    }
    public void OpenMenu()
    {
        handObjects = GameObject.FindGameObjectsWithTag("Hands"); //doing this to avoid waiting to spawn the hand prefab before checking for the objects
        if (canvasPrefabObject.activeSelf) //if menu is open
        {
            foreach (GameObject number in handObjects)
		{
			number.layer = LayerMask.NameToLayer("Default");
		}
            canvasPrefabObject.SetActive(false);
            postProcessingVolume.SetActive(false);
            return;
        }
        else if (!canvasPrefabObject.activeSelf) //if menu is not open
        {
            foreach (GameObject number in handObjects)
		{
			number.layer = LayerMask.NameToLayer("Hands");
		}
            postProcessingVolume.SetActive(true);
            canvasPrefabObject.SetActive(true);
            canvasPrefabObject.transform.LookAt(gameCamera.transform, Vector3.up);
            canvasPrefabObject.transform.Rotate(0, 180, 0);
            canvasPrefabObject.transform.position =
            (gameCamera.transform.position + gameCamera.transform.forward * menuDistance);
            return;
        }

    }
    public void ButtonPress()
    {
        Debug.Log("PRESSED");
    }
}
