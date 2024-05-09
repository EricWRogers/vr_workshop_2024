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

    private GameObject waterImpactEffect;
    private GameObject regularImpactEffect;
    [HideInInspector]
    public GameObject trailEffect;
    private Rigidbody rb;
    private bool firstContact;
    private Collider sphereCollider;

    private void Awake()
    {
        fireEffects = transform.GetChild(0).gameObject;
        trailEffect = transform.GetChild(7).gameObject;
        regularImpactEffect = transform.GetChild(10).gameObject;
        waterImpactEffect = transform.GetChild(11).gameObject;
        rb = GetComponent<Rigidbody>();
        sphereCollider = transform.GetChild(9).GetComponent<SphereCollider>();

        regularImpactEffect.SetActive(false);
        waterImpactEffect.SetActive(false);
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
        if (!firstContact && hasBeenFired)
        {
            RaycastHit hit;
            if (Physics.Raycast(sphereCollider.transform.position, sphereCollider.transform.forward, out hit, 1.0f)) //, 0, QueryTriggerInteraction.Ignore  the integer between the float and querytriggerinteraction is the layer number, the default layer. it will ignore any other.
            {
                //Debug.Log(hit.collider.gameObject);
                if (hit.transform.CompareTag("Water"))
                {
                    firstContact = true;
                    waterImpactEffect.SetActive(true);
                    AudioManager.instance.PlayAtPosition("Water_splash", hit.point);
                    //Makes the arrow disappear but keeps the trail
                    GetComponent<BoxCollider>().enabled = false;
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(2).gameObject.SetActive(false);
                    transform.GetChild(3).gameObject.SetActive(false);
                    transform.GetChild(4).gameObject.SetActive(false);
                    transform.GetChild(5).gameObject.SetActive(false);
                }
                else if ((!hit.collider.gameObject.CompareTag("NonStick") && !hit.collider.gameObject.CompareTag("Bow") && !hit.collider.gameObject.CompareTag("FireZone")) && arrowNocked == false && !hit.collider.isTrigger && hit.collider.excludeLayers != gameObject.layer)
                {
                    firstContact = true;
                    //arrowRigidbody.velocity = Vector3.zero;
                    transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    regularImpactEffect.SetActive(true);
                    //boxCollider.enabled = false; //error! the targets check for the box collider to trigger them!
                    if (hit.transform.CompareTag("Target"))
                    {
                        AudioManager.instance.Play("Target_hit");
                        if (hit.transform.gameObject.GetComponent<TargetPractice>() != null)
                        {
                            hit.transform.gameObject.GetComponent<TargetPractice>().GotHit();
                        }
                        if (hit.transform.gameObject.GetComponent<BridgeTargets>() != null)
                        {
                            hit.transform.gameObject.GetComponent<BridgeTargets>().DestroyRope();
                        }
                        if (hit.transform.gameObject.GetComponent<FirstTargets>() != null)
                        {
                            hit.transform.gameObject.GetComponent<FirstTargets>().HitTarget();
                        }
                        if (hit.transform.gameObject.GetComponent<SecondTargets>() != null)
                        {
                            hit.transform.gameObject.GetComponent<SecondTargets>().HitTarget();
                        }
                        if (hit.transform.gameObject.GetComponent<UpdatedTargetLogic>() != null)
                        {
                            hit.transform.gameObject.GetComponent<UpdatedTargetLogic>().StartPuzzleSolver();
                        }
                    }
                    else
                    {
                        AudioManager.instance.PlayAtPosition("Stone_Impact", hit.point);
                    }
                }
                else if (hit.collider.gameObject.CompareTag("NonStick"))
                {
                    //Debug.Log("Hit nonstick");
                    //AudioManager.instance.PlayAtPosition("Metal_Impact", hit.point);
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
        if (GetComponentInChildren<AudioSource>())
        {
            GetComponentInChildren<AudioSource>().gameObject.SetActive(false);
            GetComponentInChildren<AudioSource>().transform.parent = null;
        }
    }
}