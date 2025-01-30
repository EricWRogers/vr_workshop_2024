using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractArrow : MonoBehaviour
{
    public enum arrow_Types {Normal, Fire, Earth, Ice, Wind};
    public bool arrowNocked = false;
    public GameObject nockPoint;
    public GameObject attachedObject;
    public bool arrowAttached = false;
    public bool hasBeenFired = false;
    public LayerMask mask;
    public arrow_Types arrowType;

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

        trailEffect = transform.GetComponentInChildren<TrailRenderer>().gameObject;
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
        if (arrowAttached)
        {
            transform.position = attachedObject.transform.position;
            transform.rotation = attachedObject.transform.rotation;
        }

    }

    private void FixedUpdate()
    {
        //Gets front of arrow and has regular impact follow
        Vector3 predictedPos = new Vector3(m_TerrainImpactEffect.transform.position.x + m_rb.velocity.x * Time.deltaTime, m_TerrainImpactEffect.transform.position.y + m_rb.velocity.y * Time.deltaTime, m_TerrainImpactEffect.transform.position.z + m_rb.velocity.z * Time.deltaTime);
        //Linecast that gets where the arrow will shoot
        if(Physics.Linecast(nockPoint.transform.position, predictedPos, out RaycastHit hit))
        {
            if (!m_firstContact && hasBeenFired)
            {
                Transform hitTransform = hit.transform;

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
                    if (hit.transform.CompareTag("Target"))
                    {
                        
                        AudioManager.instance.Play("Target_Hit");
                        if(arrowType == arrow_Types.Fire && hitTransform.GetComponent<TheTargetScript>().needsFire)
                        {
                            hitTransform.GetComponent<TheTargetScript>().Hit();
                        }
                        if (arrowType == arrow_Types.Normal && !hitTransform.GetComponent<TheTargetScript>().needsFire)
                        {
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

    public void NockArrow(bool nocked)
    {
        arrowNocked = nocked;
    }

    public void Attach(GameObject attachObject)
    {
        attachedObject = attachObject;
        transform.SetParent(attachedObject.transform);
        arrowAttached = true;
    }

}
