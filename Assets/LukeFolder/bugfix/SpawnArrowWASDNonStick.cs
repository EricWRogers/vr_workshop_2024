using UnityEngine;

public class SpawnArrowWASDNonStick : MonoBehaviour
{
    public GameObject prefabarrow;
    Rigidbody arr_rigidbody;
    private GameObject arrow;
    public float thrust = 5.0f;
    public Transform spawnLocation;
    public Transform arrowParent;
    public bool canShoot = true;
    public Collider circleCollider;

    void Start()
    {
        arr_rigidbody = prefabarrow.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //When just using "transform" it means this.transform
        bool keyDownFlag = false;
        if (canShoot)
        {
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
            keyDownFlag = false;
        }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other);
        if (other.CompareTag("NonStick"))
        {
            //Debug.Log("Hit a nonstick object");
        }
    }
}