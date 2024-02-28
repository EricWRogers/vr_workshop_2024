using UnityEngine;

public class SpawnArrow : MonoBehaviour
{
    public GameObject prefarrow;
    Rigidbody arr_rigidbody;
    private GameObject arrow;
    public float thrust = 5.0f;
    public float spawnDistance = 1.0f;

    void Start()
    {
        arr_rigidbody = prefarrow.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //When just using "transform" it means this.transform
        bool keyDownFlag = false;
        if (Input.GetKeyDown(KeyCode.Mouse0) && keyDownFlag == false)
        {
            Vector3 playerPos = transform.position;
            Vector3 playerDirection = transform.forward;
            Quaternion playerRotation = transform.rotation;
            Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
            arrow = Instantiate(prefarrow, spawnPos, playerRotation, transform);
            arr_rigidbody = arrow.GetComponent<Rigidbody>();
            //arrow.transform.rotation = Transform.LookAt();
            //Arrow.transform.Rotate(new Vector3.forward * 45f);
            keyDownFlag = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            arrow.transform.parent = null;
            arrow.GetComponent<Rigidbody>().isKinematic = false;
            arr_rigidbody.AddForce(transform.forward * thrust, ForceMode.Impulse);
            keyDownFlag = false;
        }
    }
}