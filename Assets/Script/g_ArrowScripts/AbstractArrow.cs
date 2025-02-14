using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class AbstractArrow : MonoBehaviour
{
   
    public bool arrowNocked = false;
    public GameObject arrowNockPoint;
    public GameObject attachedObject;
    public bool arrowAttached = false;
    public bool hasBeenFired = false;
    public LayerMask mask;
    public ArrowTypes.arrow_Types arrowType;
    public GameObject iceBlockPrefab;

    [HideInInspector]
    public GameObject trailEffect;
    private Rigidbody m_rb;
    private bool m_firstContact;
    [SerializeField]
    private GameObject m_arrowGraphics;
    [SerializeField]
    private GameObject m_waterImpactEffect;
    [SerializeField]
    private GameObject m_TerrainImpactEffect;


    private void Awake()
    {
        arrowType = GameObject.FindWithTag("Player").GetComponent<ArrowTypes>().typesOfArrow;
        trailEffect = transform.GetComponentInChildren<TrailRenderer>().gameObject;
        m_rb = GetComponent<Rigidbody>();
        /*
        m_TerrainImpactEffect = GameObject.Find("Terrain_Impact");
        m_waterImpactEffect = GameObject.Find("Water_Impact");
        m_arrowGraphics = GameObject.Find("ArrowGraphic");
        */
    }

    void Start()
    {
        
    }

    void Update()
    {
        arrowType = GameObject.FindWithTag("Player").GetComponent<ArrowTypes>().typesOfArrow;
        

        if (arrowAttached)
        {
            //transform.position = attachedObject.transform.position;
            //transform.rotation = attachedObject.transform.rotation;
        }

    }

    private void FixedUpdate()
    {
        //Gets front of arrow and has regular impact follow
        Vector3 predictedPos = new Vector3(m_TerrainImpactEffect.transform.position.x + m_rb.velocity.x * Time.deltaTime, m_TerrainImpactEffect.transform.position.y + m_rb.velocity.y * Time.deltaTime, m_TerrainImpactEffect.transform.position.z + m_rb.velocity.z * Time.deltaTime);
        //Linecast that gets where the arrow will shoot
        if(Physics.Linecast(arrowNockPoint.transform.position, predictedPos, out RaycastHit hit))
        {
            if (!m_firstContact && hasBeenFired)
            {
                Transform hitTransform = hit.transform;
                transform.parent = null;

                if (hitTransform.CompareTag("Water"))
                {
                    m_firstContact = true;
                    m_waterImpactEffect.SetActive(true);
                    m_rb.constraints = RigidbodyConstraints.FreezeAll;
                    m_arrowGraphics.SetActive(false);
                }
                else if (hitTransform.CompareTag("NonStick"))
                {
                    //stop the arrow and make it fall
                }
                else if(!hitTransform.CompareTag("Bow") && arrowNocked == false && !hit.collider.isTrigger && hit.collider.excludeLayers != gameObject.layer)
                {
                    m_firstContact = true;
                    m_TerrainImpactEffect.SetActive(true);
                    transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    m_rb.constraints = RigidbodyConstraints.FreezeAll;
                    TheTargetScript target = hitTransform.GetComponent<TheTargetScript>();
                    if (hit.transform.CompareTag("Target"))
                    {
                        
                        AudioManager.instance.Play("Target_hit");
                        //calls the targets event when hit with respective arrow

                        //normal
                        if(arrowType == ArrowTypes.arrow_Types.Normal && target.arrowRequired == TheTargetScript.arrow_Types.Normal)
                        {
                            Debug.Log("normal");
                            //Attach(hitTransform.gameObject);
                            hitTransform.GetComponent<TheTargetScript>().Hit();
                        }
                        //fire
                        else if (arrowType == ArrowTypes.arrow_Types.Fire && target.arrowRequired == TheTargetScript.arrow_Types.Fire)
                        {
                            Debug.Log("fire");
                            //Attach(hitTransform.gameObject);
                            hitTransform.GetComponent<TheTargetScript>().Hit();
                        }
                        //ice
                        else if(arrowType == ArrowTypes.arrow_Types.Ice && target.arrowRequired == TheTargetScript.arrow_Types.Ice)
                        {
                            Debug.Log("ice");
                            Debug.Log("Spawning Ice Block");
                            //Attach(hitTransform.gameObject);
                            hitTransform.GetComponent<TheTargetScript>().Hit();
                            SpawnIceBlock(hit.point, hit.normal);
                        }
                        //earth
                        else if(arrowType == ArrowTypes.arrow_Types.Earth && target.arrowRequired == TheTargetScript.arrow_Types.Earth)
                        {   
                            Debug.Log("earth");
                            //Attach(hitTransform.gameObject);
                            hitTransform.GetComponent<TheTargetScript>().Hit();
                        }
                        //wind
                        else if(arrowType == ArrowTypes.arrow_Types.Wind && target.arrowRequired == TheTargetScript.arrow_Types.Wind)
                        {
                            Debug.Log("wind");
                            //Attach(hitTransform.gameObject);
                            hitTransform.GetComponent<TheTargetScript>().Hit();
                        }
                    }
                }
                else
                {
                    AudioManager.instance.PlayAtPosition("Stone_Impact", hit.point);
                }
            }
        }
        // Look in the direction we are moving
        if (hasBeenFired && (m_rb.velocity.z > 0.5f || m_rb.velocity.y > 0.5f || m_rb.velocity.x > 0.5f))
        {
            transform.forward = m_rb.velocity;
        }

    }

    private void SpawnIceBlock(Vector3 position, Vector3 normal)
    {
        if (iceBlockPrefab != null)
        {
            Quaternion rotation = Quaternion.LookRotation(normal);
            GameObject iceBlock = Instantiate(iceBlockPrefab, position, rotation);
        
            IceBlock iceBlockScript = iceBlock.GetComponent<IceBlock>();
            if (iceBlockScript != null)
            {
                iceBlockScript.StartGrowing();
            }
            else
            {
                Debug.LogWarning("IceBlock script is missing from the IceBlock prefab!");
            }
        }
        else
        {
            Debug.LogError("Ice Block Prefab not assigned in AbstractArrow!");
        }
    }
    public void NockArrow(bool nocked)
    {
        arrowNocked = nocked;
    }

    public void Attach(GameObject attachObject)
    {
        attachedObject = attachObject;
        transform.position = attachedObject.transform.position;
        transform.rotation = attachedObject.transform.rotation;
        transform.SetParent(attachedObject.transform);
        arrowAttached = true;
    }

    public void NormalArrow()
    {
        
    }

    public void FireArrow()
    {

    }

    public void IceArrow()
    {

    }

    public void EarthArrow()
    {

    }

    public void WindArrow()
    {

    }
    public void Destroy()
    {
        Destroy(gameObject);
    }

}
