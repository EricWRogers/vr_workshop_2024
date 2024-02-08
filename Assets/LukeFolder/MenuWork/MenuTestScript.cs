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
    void Start()
    {
        
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
                }
                
        }
        if (Input.GetKeyUp("`"))
        {
            keyDown = false;
        }

        if(canvasObject.activeSelf)
        {
            this.transform.position = gameCamera.transform.position + new Vector3(0,0,menuDistance);

            //this.transform.LookAt(gameCamera.transform, Vector3.up);

        }



    }
    public void ButtonClick()
    {
        Debug.Log("Clicked");
    }
}
