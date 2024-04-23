using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutRope : MonoBehaviour
{
    public GameObject prefab;


    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Arrow"))
        {
            Debug.Log(GetComponent<Collider>().gameObject.transform.position);
            //Instantiate(prefab, )
            Debug.Log("Aidens Arrow Collided");
            Destroy(this.gameObject);
        }
        
    }
}
