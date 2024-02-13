using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;

public class pressurePlateScript : MonoBehaviour
{
    public GameObject targetObject;
    public UnityEvent activatorEvent; //on activate
    public UnityEvent deactivatorEvent; //on deactivate
    Collider lastCollided;
    bool buttonActive = false; //is button active? only one object should activate it at a time?
    public GameObject buttonObject;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider colliderEnter)
    {   
        if(buttonActive == false)
        {
            lastCollided = colliderEnter; //save what last entered, gonna check it on exit
        
            Debug.Log(colliderEnter.gameObject);
            //check for tag

            activatorEvent.Invoke();
            buttonActive = true;
            buttonObject.SetActive(false);
        }
        
    }
    void OnTriggerExit(Collider colliderExit) //ISSUE: IF OBJECT LEAVES BY METHOD OF DEACTIVATION OR DELETION, IT WILL NOT TRIGGER ON EXIT
    {

            Debug.Log("Object that entered first left");
            deactivatorEvent.Invoke();
            buttonActive = false;
            buttonObject.SetActive(true);
    }
}
