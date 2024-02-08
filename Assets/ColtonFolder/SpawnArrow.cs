using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArrow : MonoBehaviour
{
    public GameObject Arrow;
    Rigidbody arr_rigidbody;

    public float thrust = 2.0f;
    void Start()
    {
        arr_rigidbody = Arrow.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        bool keyDownFlag = false;
        if (Input.GetKeyDown(KeyCode.Mouse0) && keyDownFlag == false)
        {
            GameObject arrow = Instantiate(Arrow, (transform.position + new Vector3 (0,1.5f, 2.0f)), Quaternion.identity);
            //arrow.transform.rotation = Transform.LookAt();
           // Arrow.transform.Rotate(new Vector3.forward * 45f);
            keyDownFlag = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //Debug.Log("Key up");
            arr_rigidbody.AddForce(transform.up * thrust, ForceMode.Impulse);
            Arrow.GetComponent<Rigidbody>().isKinematic = false;
            keyDownFlag = false;
        }
    }
}
