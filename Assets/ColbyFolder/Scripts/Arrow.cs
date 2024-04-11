using UnityEngine;

public class Arrow : MonoBehaviour
{
    private GameObject player;
    private GameObject fireEffects;
    public float fireTimer = 0.0f;
    public bool arrowNocked = false;
    public float lengthOfFire = 8.0f;
    public bool onFire = false;

    //Aiden's Addition to the Code
    public GameObject trailEffect;
    public bool hasBeenFired = false;
    public LayerMask mask;
    private Vector3 positionLastFrame;
    private RaycastHit info;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fireEffects = transform.GetChild(0).gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Bow"))
        {
            player.GetComponent<SpawnArrowVR>().arrowNocked = true;
            arrowNocked = true;
        }
        else if (other.CompareTag("FireZone"))
        {
            onFire = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bow"))
        {
            if (player != null && player.GetComponent<SpawnArrowVR>() != null)
            {
                player.GetComponent<SpawnArrowVR>().arrowNocked = false;
                arrowNocked = false;
            }
            // player.GetComponent<SpawnArrowVR>().arrowNocked = false;
            // arrowNocked = false;
        }else if(other.CompareTag("FireZone"))
        {
            if (player != null && player.GetComponent<SpawnArrowVR>() != null && player.GetComponent<SpawnArrowVR>().arrowNocked)
            {
                onFire = false;
            }
        }
        // else if (other.CompareTag("FireZone") && player.GetComponent<SpawnArrowVR>().arrowNocked)
        // {
        //     onFire = false;
        // }
    }

    private void Update()
    {
        fireTimer -= Time.deltaTime;
        if (!arrowNocked && onFire && fireTimer < 0.0f)
        {
            onFire = false;
        }

        //Aiden Added This
        if(trailEffect != null)
        {
            if(hasBeenFired == true)
            {
                trailEffect.SetActive(true);
            }else
            {
                trailEffect.SetActive(false);
            }
        }//

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