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
    [HideInInspector]
    public GameObject trailEffect;
    private Vector3 positionLastFrame;
    private RaycastHit info;

    private void Awake()
    {
        fireEffects = transform.GetChild(0).gameObject;
        trailEffect = transform.GetChild(7).gameObject;
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
        if (Physics.Linecast(positionLastFrame, transform.position, out info, mask))
        {
            if (info.transform.CompareTag("Target"))
            {
                if(info.transform.gameObject.GetComponent<TargetPractice>() != null)
                {
                    info.transform.gameObject.GetComponent<TargetPractice>().GotHit();
                }
                if(info.transform.gameObject.GetComponent<BridgeTargets>() != null)
                {
                    info.transform.gameObject.GetComponent<BridgeTargets>().DestroyRope();
                }
                if(info.transform.gameObject.GetComponent<FirstTargets>() != null)
                {
                    info.transform.gameObject.GetComponent<FirstTargets>().HitTarget();
                }
                if(info.transform.gameObject.GetComponent<SecondTargets>() != null)
                {
                    info.transform.gameObject.GetComponent<SecondTargets>().HitTarget();
                }
                if(info.transform.gameObject.GetComponent<UpdatedTargetLogic>() != null)
                {
                    info.transform.gameObject.GetComponent<UpdatedTargetLogic>().StartPuzzleSolver();
                }
                
            }

        }

        positionLastFrame = transform.position;

        // Look in the direction we are moving
        if (!arrowAttached)
        {
            transform.LookAt(positionLastFrame);
        }
    }
}