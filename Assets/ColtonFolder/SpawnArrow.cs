using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArrow : MonoBehaviour
{
    public GameObject Arrow;

    public float thrust = 2.0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool keyDownFlag = false;
        if (Input.GetKeyDown(KeyCode.Mouse0) && keyDownFlag == false)
        {
            Instantiate(Arrow, new Vector3(0, 0, 0), Quaternion.identity);
            keyDownFlag = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Debug.Log("Launch arrow");
            keyDownFlag = false;
        }
    }
}
