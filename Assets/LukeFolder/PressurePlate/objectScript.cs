using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class objectScript : MonoBehaviour
{
    void Start()
    {

    }
    void Update()
    {
        
    }

    public void Activate()
    {
        Debug.Log("Object is active");
    }
    public void Deactivate()
    {
        Debug.Log("Object has been deactivated");
    }
}
