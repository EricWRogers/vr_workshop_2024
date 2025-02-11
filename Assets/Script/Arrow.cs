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
    public float distanceAwayFromFireToLight = 10f;

    private GameObject waterImpactEffect;
    private GameObject regularImpactEffect;
    [HideInInspector]
    public GameObject trailEffect;
    private Rigidbody rb;
    private bool firstContact;

    private void Awake()
    {
        fireEffects = transform.GetChild(0).gameObject;
        trailEffect = transform.GetChild(7).gameObject;
        regularImpactEffect = transform.GetChild(9).gameObject;
        waterImpactEffect = transform.GetChild(10).gameObject;
        rb = GetComponent<Rigidbody>();

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
        //Where the front of the arrow will be next frame
        Vector3 predictedPosition = new Vector3(transform.GetChild(9).position.x + rb.velocity.x*Time.deltaTime, transform.GetChild(9).position.y + rb.velocity.y*Time.deltaTime, transform.GetChild(9).position.z + rb.velocity.z*Time.deltaTime);
        //Linecast from the back of the arrow to front
        if (Physics.Linecast(transform.GetChild(8).position, predictedPosition, out RaycastHit hit, mask))
        {
            if (!firstContact && hasBeenFired)
            {
                Transform hitTransform = hit.transform;
                //Debug.Log(hit.collider.gameObject);
                if (hitTransform.CompareTag("Water"))
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
                else if (hitTransform.CompareTag("NonStick"))
                {
                    //Debug.Log("Hit nonstick");
                    //AudioManager.instance.PlayAtPosition("Metal_Impact", hit.point);
                }
                else if ((!hitTransform.CompareTag("Bow") && !hitTransform.CompareTag("FireZone")) && arrowNocked == false && !hit.collider.isTrigger && hit.collider.excludeLayers != gameObject.layer)
                {
                    //Debug.Log("Stick to " + hit.transform.name);
                    firstContact = true;
                    regularImpactEffect.SetActive(true);
                    //arrowRigidbody.velocity = Vector3.zero;
                    transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    //transform.parent = hitTransform;
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    //boxCollider.enabled = false; //error! the targets check for the box collider to trigger them!
                    if (hit.transform.CompareTag("Target"))
                    {
                        AudioManager.instance.Play("Target_hit");
                        if (hitTransform.GetComponent<TargetPractice>())
                        {
                            hitTransform.GetComponent<TargetPractice>().GotHit();
                        }
                        else if (hitTransform.GetComponent<BridgeTargets>())
                        {
                            hitTransform.GetComponent<BridgeTargets>().DestroyRope();
                        }
                        else if (hitTransform.GetComponent<FirstTargets>())
                        {
                            hitTransform.GetComponent<FirstTargets>().HitTarget();
                        }
                        else if (hitTransform.GetComponent<SecondTargets>())
                        {
                            hitTransform.GetComponent<SecondTargets>().HitTarget();
                        }
                        else if (hitTransform.GetComponent<UpdatedTargetLogic>())
                        {
                            hitTransform.GetComponent<UpdatedTargetLogic>().StartPuzzleSolver();
                        }
                        else if (onFire)
                        {
                            if (hitTransform.GetComponent<FireTargets>())
                            {
                                hitTransform.GetComponent<FireTargets>().OnHit();
                            }
                            else if (hitTransform.GetComponent<FlameableTree>())
                            {
                                hitTransform.GetComponent<FlameableTree>().OnHit();
                            }
                            else if (hitTransform.GetComponent<ExplosiveBarrel>())
                            {
                                hitTransform.GetComponent<ExplosiveBarrel>().Detonate();
                            }
                        }
                    }
                    else
                    {
                        AudioManager.instance.PlayAtPosition("Stone_Impact", hit.point);
                    }
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
            GameObject obj = GetComponentInChildren<AudioSource>().gameObject;
            obj.transform.parent = null;
            obj.SetActive(false);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}