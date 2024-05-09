using System.Xml;
using UnityEngine;

public class TeleportArrow : MonoBehaviour
{
    public bool arrowNocked = false;
    public GameObject attachedObject;
    public bool arrowAttached = false;
    public bool hasBeenFired = false;
    public LayerMask mask;
    [HideInInspector]
    public GameObject trailEffect;
    private RaycastHit info;
    Rigidbody rb;
    private Transform player;

    private void Awake()
    {
        trailEffect = transform.GetChild(7).gameObject;
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
    }

    //For improved hit detection
    private void FixedUpdate()
    {
        if (hasBeenFired)
        {
            Vector3 predictedPosition = new Vector3(transform.position.x + rb.velocity.x, transform.position.y + rb.velocity.y, transform.position.z + rb.velocity.z);
            if (Physics.Raycast(transform.position, transform.forward, out info, Vector3.Distance(transform.position, predictedPosition)))
            {
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
                    player.position = info.point + (info.normal * 1.5f);
                    Destroy(gameObject);
                }
            }

            // Look in the direction we are moving
            if (rb.velocity.z > 0.5f || rb.velocity.y > 0.5f || rb.velocity.x > 0.5f)
            {
                transform.forward = rb.velocity;
            }
        }
    }

    private void OnDestroy()
    {
        if (GetComponentInChildren<AudioSource>() != null)
        {
            GetComponentInChildren<AudioSource>().gameObject.SetActive(false);
            if (GetComponentInChildren<AudioSource>().GetComponent<Transform>())
            {
                GetComponentInChildren<AudioSource>().transform.parent = null;
            }
        }
    }
}
