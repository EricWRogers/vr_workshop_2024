using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArrow : MonoBehaviour
{
    public GameObject prefarrow;
    Rigidbody arr_rigidbody;
    private GameObject arrow;

    public float thrust = 10.0f;
    void Start()
    {
        arr_rigidbody = prefarrow.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        bool keyDownFlag = false;
        if (Input.GetKeyDown(KeyCode.Mouse0) && keyDownFlag == false)
        {
            arrow = Instantiate(prefarrow, (transform.position + new Vector3 (0,1.5f, 2.0f)), Quaternion.identity);
            arr_rigidbody = arrow.GetComponent<Rigidbody>();
            //arrow.transform.rotation = Transform.LookAt();
           // Arrow.transform.Rotate(new Vector3.forward * 45f);
            keyDownFlag = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //Debug.Log("Key up");
            arr_rigidbody.AddForce(transform.up * thrust, ForceMode.Impulse);
            arr_rigidbody.AddForce(transform.forward * thrust, ForceMode.Impulse);
            arrow.GetComponent<Rigidbody>().isKinematic = false;
            keyDownFlag = false;
        }
    }
}
