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
    public bool inFireZone = false;

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
            arrow.transform.rotation = arrowParent.transform.rotation;
            arrow.transform.parent = arrowParent;
            arr_rigidbody = arrow.GetComponent<Rigidbody>();
            arrow.GetComponent<Arrow>().arrowNocked = true;
            arrow.GetComponent<Arrow>().trailEffect.SetActive(false);
            if (inFireZone)
            {
                arrow.GetComponent<Arrow>().onFire = true;
            }
            keyDownFlag = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            arrow.transform.parent = null;
            arrow.GetComponent<Rigidbody>().isKinematic = false;
            arr_rigidbody.AddForce(arrow.transform.forward * thrust, ForceMode.Impulse);
            if (arrow.GetComponent<Arrow>().onFire )
            {
                arrow.GetComponent<Arrow>().fireTimer = arrow.GetComponent<Arrow>().lengthOfFire;
            }
            arrow.GetComponent<Arrow>().arrowNocked = false;
            arrow.GetComponent<Arrow>().hasBeenFired = true;
            if(arrow.GetComponent<Arrow>().hasBeenFired == true)
            {
                arrow.GetComponent<Arrow>().trailEffect.SetActive(true);
            }
            keyDownFlag = false;
            AudioManager.instance.Play("Arrow_Whoosh");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FireZone"))
        {
            inFireZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FireZone"))
        {
            inFireZone = false;
        }
    }
}