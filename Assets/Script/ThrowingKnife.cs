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

    public float maxThrowDistance;
    private float curThrowDistance;
    public LayerMask layerMask;
    private RaycastHit hit;
    public Vector3 lastPos;
    public Vector3 curPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {   
       
        curThrowDistance = Vector3.Distance(transform.position, player.transform.position);
        lastPos = curPos;
        curPos = transform.position;
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
}
