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

    //Aiden added this
    private void FixedUpdate()
    {
        if (Physics.Linecast(positionLastFrame, transform.position, out info, mask))
        {
            if (info.transform.CompareTag("Target"))
            {
                Debug.Log("Target was detected");
                if(info.transform.gameObject.GetComponent<TargetPractice>() != null)
                {
                    info.transform.gameObject.GetComponent<TargetPractice>().GotHit();
                    Debug.Log("Target has changed color");
                }
                if(info.transform.gameObject.GetComponent<BridgeTargets>() != null)
                {
                    info.transform.gameObject.GetComponent<BridgeTargets>().DestroyRope();
                    Debug.Log("Target Destroyed");
                }
                if(info.transform.gameObject.GetComponent<FirstTargets>() != null)
                {
                    info.transform.gameObject.GetComponent<FirstTargets>().HitTarget();
                    Debug.Log("Targets was hit");
                }
                if(info.transform.gameObject.GetComponent<SecondTargets>() != null)
                {
                    info.transform.gameObject.GetComponent<SecondTargets>().HitTarget();
                    Debug.Log("Target was hit");
                }
                if(info.transform.gameObject.GetComponent<UpdatedTargetLogic>() != null)
                {
                    info.transform.gameObject.GetComponent<UpdatedTargetLogic>().StartPuzzleSolver();
                    Debug.Log("Target was hit");
                }
                
            }

        }

        positionLastFrame = transform.position;
    }//
}