using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTurntable : MonoBehaviour
{
    //public GameObject turnTable;

    public float rotateSpeed;

    public bool isPuzzleDone = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPuzzleDone)
        {
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }
    }
}
