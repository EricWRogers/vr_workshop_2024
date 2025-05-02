using System.Collections;
using System.Collections.Generic;
using SuperPupSystems.Helper;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowingKnife : MonoBehaviour
{
    public GameObject player;

    public float checkerYOffset;
    public float checkerXOffset;
    public float checkerZOffset;
    public float checkerRadius;
    public float checkerDistance;

    public float maxThrowDistance = 20f;
    private float curThrowDistance;
    public LayerMask layerMask;
    public LayerMask collisionLayer;
    private RaycastHit hit;
    private Rigidbody rb;
    private bool hasLanded = false;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindWithTag("Player");
        Throw(transform.forward, 15f); // Optional default throw, you can call externally too
    }

    // Update is called once per frame
    void Update()
    {
        curThrowDistance = Vector3.Distance(transform.position, player.transform.position);
            
        if (!hasLanded)
        {
            // Optional: Rotate to face movement direction for realistic look
            if (rb.velocity != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        
    }

    public void CheckToTeleport()
    {
        Debug.Log("Try teleport");
        if (GetComponent<Bullet>().hitInfo.collider.gameObject.GetComponent<TeleportCrystal>())
        {
            GetComponent<Bullet>().hitInfo.collider.gameObject.GetComponent<TeleportCrystal>().TeleportToCrystal();
            return;
        }
        if (curThrowDistance < maxThrowDistance)
        {
        Teleport();
        }

           
    }

    public void Teleport()
    {
        Debug.Log("teleport");
        if (player.GetComponent<SpawnKnife>().canTeleport)
        {
            player.transform.position = transform.position + (-transform.forward);
        }
    }

    public void Throw(Vector3 direction, float force)
    {
        rb.isKinematic = false;
        rb.AddForce(direction * force, ForceMode.VelocityChange);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & collisionLayer) != 0)
        {
            hasLanded = true;
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.parent = collision.transform; // Stick to surface

            CheckToTeleport(collision);
        }
    }

    void CheckToTeleport(Collision collision)
    {
        float curThrowDistance = Vector3.Distance(transform.position, player.transform.position);
        
        TeleportCrystal crystal = collision.collider.GetComponent<TeleportCrystal>();
        if (crystal != null)
        {
            crystal.TeleportToCrystal();
            return;
        }

        if (curThrowDistance <= maxThrowDistance)
        {
            Teleport();
        }
    }
}