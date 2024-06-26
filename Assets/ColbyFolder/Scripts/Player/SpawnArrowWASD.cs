using UnityEngine;

public class SpawnArrowWASD : MonoBehaviour
{
    public GameObject prefabarrow;
    Rigidbody arr_rigidbody;
    private GameObject arrow;
    public float thrust = 5.0f;
    public Transform spawnLocation;
    public Transform arrowParent;
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
            arrow = Instantiate(prefabarrow, spawnLocation.position, arrowParent.rotation, arrowParent);
            arr_rigidbody = arrow.GetComponent<Rigidbody>();
            if (arrow.GetComponent<Arrow>())
            {
                arrow.GetComponent<Arrow>().arrowNocked = true;
                arrow.GetComponent<Arrow>().trailEffect.SetActive(false);
                if (inFireZone)
                {
                    arrow.GetComponent<Arrow>().onFire = true;
                }
            }
            else
            {
                arrow.GetComponent<TeleportArrow>().arrowNocked = true;
                arrow.GetComponent<TeleportArrow>().trailEffect.SetActive(false);
            }
            
            keyDownFlag = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            arrow.transform.parent = null;
            arrow.GetComponent<Rigidbody>().isKinematic = false;
            arr_rigidbody.AddForce(arrow.transform.forward * thrust, ForceMode.Impulse);
            if (arrow.GetComponent<Arrow>())
            {
                if (arrow.GetComponent<Arrow>().onFire)
                {
                    arrow.GetComponent<Arrow>().fireTimer = arrow.GetComponent<Arrow>().lengthOfFire;
                }
                arrow.GetComponent<Arrow>().arrowNocked = false;
                arrow.GetComponent<Arrow>().hasBeenFired = true;
                if (arrow.GetComponent<Arrow>().hasBeenFired == true)
                {
                    arrow.GetComponent<Arrow>().trailEffect.SetActive(true);
                }
            }
            else
            {
                arrow.GetComponent<TeleportArrow>().arrowNocked = false;
                arrow.GetComponent<TeleportArrow>().hasBeenFired = true;
                if (arrow.GetComponent<TeleportArrow>().hasBeenFired)
                {
                    arrow.GetComponent<TeleportArrow>().trailEffect.SetActive(true);
                }
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