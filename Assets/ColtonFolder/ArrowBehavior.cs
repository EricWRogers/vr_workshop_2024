using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject Arrow;
    public GameObject player;
    public Rigidbody arb;

    public float thrust = 1.0f;
    void Start()
    {
        arb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(Arrow, new Vector3(player.transform.position.x + 1,
                player.transform.position.y,
                1.0f), Quaternion.identity);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            arb.AddForce(transform.forward * thrust);
        }

    }
}
