using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeDrawBridge : MonoBehaviour
{
    public Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DrawBridge"))
        {
            Debug.Log("The Bridge will stop moving now"); 
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
