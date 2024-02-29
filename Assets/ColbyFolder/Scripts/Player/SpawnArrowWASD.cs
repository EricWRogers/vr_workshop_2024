using UnityEngine;

public class SpawnArrowWASD : MonoBehaviour
{
    public GameObject prefabarrow;
    Rigidbody arr_rigidbody;
    private GameObject arrow;
    public float thrust = 5.0f;
    public Transform spawnLocation;
    public Transform arrowParent;

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
            arrow = Instantiate(prefabarrow, spawnLocation.position, arrowParent.rotation, arrowParent);
            arr_rigidbody = arrow.GetComponent<Rigidbody>();
            keyDownFlag = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            arrow.transform.parent = null;
            arrow.GetComponent<Rigidbody>().isKinematic = false;
            arr_rigidbody.AddForce(arrow.transform.forward * thrust, ForceMode.Impulse);
            Destroy(arrow, 3.5f);
            keyDownFlag = false;
        }
    }
}