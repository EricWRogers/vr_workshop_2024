using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDrawBridgeFunctionality : MonoBehaviour
{
    public GameObject rope;
    public GameObject ropeTwo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(rope == null && ropeTwo == null)
       {
            Debug.Log("The space key was pressed");
            Destroy(this.gameObject);
       }
    }
}
