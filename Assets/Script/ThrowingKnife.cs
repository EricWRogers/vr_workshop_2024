using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        curThrowDistance = Vector3.Distance(transform.position, player.transform.position);
        
    }

    public void CheckToTeleport()
    {
        if (Physics.SphereCast(new Vector3(transform.position.x + checkerXOffset, transform.position.y + checkerYOffset, transform.position.z + checkerZOffset), checkerRadius, transform.up , out hit, checkerDistance, layerMask))
        {
            return;
        }
        else
        {
            if (curThrowDistance >= maxThrowDistance)
            {
                return;
            }
            else
            {
                Teleport();
            }
        }
    }

    public void Teleport()
    {
        if (player.GetComponent<SpawnKnife>().canTeleport)
        {
            player.transform.position = transform.position + (-transform.forward);
        }
    }
}
