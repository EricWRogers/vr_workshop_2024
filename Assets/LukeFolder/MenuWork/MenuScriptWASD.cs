using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScriptWASD : MonoBehaviour
{
    public GameObject canvasObject;
    public Camera gameCamera; //FIX THIS: GET CAMERA BY GET COMPONENT, NOT THROUGH A PUBLIC "DRAG AND DROP"
    bool keyDown = false; //for the keypress toggle for the menu
    public float menuDistance = 5.0f; //meant to push the z value of the menu away from camera
    public GameObject fpsScript;

    void Start()
    {

    }


    void Update()
    {
        if (UnityEngine.XR.XRSettings.enabled == false)
        { //check if in vr
            if (Input.GetKeyDown("`") && keyDown == false)
            {
                if (canvasObject.activeSelf == true && keyDown == false) // if menu is open, close it
                {
                    canvasObject.SetActive(false);
                    keyDown = true;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    fpsScript.GetComponent<FPSController>().canMove = true;
                    fpsScript.GetComponent<SpawnArrowWASDNonStick>().canShoot = true;
                }
                if (canvasObject.activeSelf == false && keyDown == false) //if menu is closed, open it
                {
                    fpsScript.GetComponent<FPSController>().canMove = false;
                    fpsScript.GetComponent<SpawnArrowWASDNonStick>().canShoot = false; //the <*> part may need to be changed to another name of the non modified script.
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    canvasObject.SetActive(true);
                    keyDown = true;
                    /*transform.LookAt(gameCamera.transform, Vector3.up);
                    this.transform.Rotate(0,180,0);
                    transform.position = gameCamera.transform.position + gameCamera.transform.forward * menuDistance; 
                    transform.position = new Vector3(transform.position.x, gameCamera.transform.position.y, transform.position.z);*/
                }

            }
            if (Input.GetKeyUp("`"))
            {
                keyDown = false;
            }
        }
    }
    public void OpenMenu()
    {
        Debug.Log("Menu Opened in Desktop Mode");
    }
}
