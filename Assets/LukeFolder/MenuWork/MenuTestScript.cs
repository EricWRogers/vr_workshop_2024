using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuTestScript : MonoBehaviour
{
    public GameObject canvasObject;
    Camera gameCamera;
    bool keyDown = false; //for the keypress toggle for the menu
    public float menuDistance = 5.0f; //meant to push the z value of the menu away from camera
    
    void Awake()
    {
        gameCamera = Camera.main;
    }

    void Update()
    {
        
        if (Input.GetKeyDown("`") && keyDown == false)
        {
                if (canvasObject.activeSelf == true && keyDown == false)
                {
                    canvasObject.SetActive(false);
                    keyDown = true;
                }
                if (canvasObject.activeSelf == false && keyDown == false)
                {
                    canvasObject.SetActive(true);
                    keyDown = true;
                    transform.LookAt(gameCamera.transform, Vector3.up);
                    this.transform.Rotate(0,180,0);
                    transform.position = gameCamera.transform.position + gameCamera.transform.forward * menuDistance; 
                    transform.position = new Vector3(transform.position.x, gameCamera.transform.position.y, transform.position.z);
                }
                
        }
        if (Input.GetKeyUp("`"))
        {
            keyDown = false;
        }
    }
}
