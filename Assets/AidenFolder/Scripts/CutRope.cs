using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutRope : MonoBehaviour
{
    public void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("AidensArrow"))
        {
            //Debug.Log(collider.gameObject.transform.position);
            Debug.Log("Aidens Arrow Collided");
            Destroy(this.gameObject);
        }
        
    }
}
