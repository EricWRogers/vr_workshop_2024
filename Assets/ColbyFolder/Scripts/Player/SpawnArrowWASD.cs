using UnityEngine;
using System.Collections;

public class SpawnArrowWASD : MonoBehaviour
{
    public GameObject prefabarrow;
    Rigidbody arr_rigidbody;
    private GameObject arrow;
    public float thrust = 5.0f;
    public Transform spawnLocation;
    public Transform arrowParent;
    public float delayTime = 4.0f;

    void Start()
    {
        arr_rigidbody = prefabarrow.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //When just using "transform" it means this.transform
        bool keyDownFlag = false;
        if (Input.GetKeyDown(KeyCode.Mouse0) && keyDownFlag == false)
        {
            arrow = ArrowPool.SharedInstance.GetPooledObject();
            arrow.SetActive(true);
            arrow.transform.position = spawnLocation.transform.position;
            arrow.transform.rotation = spawnLocation.transform.rotation;
            arr_rigidbody = arrow.GetComponent<Rigidbody>();
            keyDownFlag = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            arrow.transform.parent = null;
            arrow.GetComponent<Rigidbody>().isKinematic = false;
            arr_rigidbody.AddForce(arrow.transform.forward * thrust, ForceMode.Impulse);
            keyDownFlag = false;
        }
    }
}