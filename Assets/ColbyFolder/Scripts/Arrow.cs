using UnityEngine;

public class Arrow : MonoBehaviour
{
    private GameObject fireEffects;
    public float fireTimer = 0.0f;
    public bool arrowNocked = false;
    public float lengthOfFire = 8.0f;
    public bool onFire = false;
    public GameObject attachedObject;
    public bool arrowAttached = false;
    public bool hasBeenFired = false;
    public LayerMask mask;

    // public GameObject waterImpactEffect;
    // public GameObject regularImpactEffect;
    [HideInInspector]
    public GameObject trailEffect;
    private RaycastHit info;
    Rigidbody rb;

    private void Awake()
    {
        fireEffects = transform.GetChild(0).gameObject;
        trailEffect = transform.GetChild(7).gameObject;
        //regularImpactEffect = transform.GetChild(8).gameObject;
        //waterImpactEffect = transform.GetChild(9).gameObject;
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("FireZone"))
        {
            onFire = true;
            fireTimer = lengthOfFire;
        }
    }

    public void NockArrow(bool nocked)
    {
        arrowNocked = nocked;
    }

    public void Attach(GameObject attachObject)
    {
        attachedObject = attachObject;
        arrowAttached = true;
    }

    private void Update()
    {
        if (arrowAttached)
        {
            transform.position = attachedObject.transform.position;
            transform.rotation = attachedObject.transform.rotation;
        }

        fireTimer -= Time.deltaTime;
        if (onFire && fireTimer < 0.0f)
        {
            onFire = false;
        }

        fireEffects.SetActive(onFire);
    }

    //For improved hit detection
    private void FixedUpdate()
    {
        Vector3 predictedPosition = new Vector3 (transform.position.x + rb.velocity.x, transform.position.y + rb.velocity.y, transform.position.z + rb.velocity.z);
        if (Physics.Linecast(transform.position, predictedPosition, out info, mask))
        {
            // Debug.Log("The Arrow Hit: " + info.transform.gameObject.name);
            // regularImpactEffect.SetActive(true);
            // if(regularImpactEffect.activeSelf)
            // {
            //     Debug.Log("Particles on: " + regularImpactEffect.name);
            // }else
            // {
            //     Debug.Log("Particles off: " + regularImpactEffect.name);
            // }
            if (info.transform.CompareTag("Target"))
            {
                if (info.transform.gameObject.GetComponent<TargetPractice>() != null)
                {
                    info.transform.gameObject.GetComponent<TargetPractice>().GotHit();
                }
                if (info.transform.gameObject.GetComponent<BridgeTargets>() != null)
                {
                    info.transform.gameObject.GetComponent<BridgeTargets>().DestroyRope();
                }
                if (info.transform.gameObject.GetComponent<FirstTargets>() != null)
                {
                    info.transform.gameObject.GetComponent<FirstTargets>().HitTarget();
                }
                if (info.transform.gameObject.GetComponent<SecondTargets>() != null)
                {
                    info.transform.gameObject.GetComponent<SecondTargets>().HitTarget();
                }
                if (info.transform.gameObject.GetComponent<UpdatedTargetLogic>() != null)
                {
                    info.transform.gameObject.GetComponent<UpdatedTargetLogic>().StartPuzzleSolver();
                }
            }
        }

        // Look in the direction we are moving
        if (hasBeenFired && (rb.velocity.z > 0.5f || rb.velocity.y > 0.5f || rb.velocity.x > 0.5f))
        {
            transform.forward = rb.velocity;
        }
    }

    private void OnDestroy()
    {
        if (GetComponentInChildren<AudioSource>() != null)
        {
            GetComponentInChildren<AudioSource>().gameObject.SetActive(false);
            GetComponentInChildren<AudioSource>().transform.parent = null;
        }
    }
}