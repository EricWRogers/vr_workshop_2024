using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiTeleportZone : MonoBehaviour
{
    public GameObject player;
    public float radius;
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
        if (Physics.SphereCast(transform.position, radius, transform.up, out hit, 0, layerMask))
        {
            player.GetComponent<SpawnKnife>().canTeleport = false;
        }
        else
        {
            player.GetComponent<SpawnKnife>().canTeleport = true;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void Destroy()
    {
        Destroy(this);
    }
}
