using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class DoorScript : MonoBehaviour
{
    public GameObject particles;
    public float heightOfDoorOpening = -6f;
    public float speed = 10f;
    private Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenDoor()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, startingPosition + new Vector3(0, heightOfDoorOpening, 0), step);
        particles.SetActive(true);
        if (Vector3.Distance(transform.position, startingPosition + new Vector3(0, heightOfDoorOpening, 0)) < 0.5f)
        {
            //It has arrived
            //AudioManager.instance.Stop("Stone_door");
            AudioManager.instance.Play("Stone_crash");
            GetComponent<AudioSource>().Stop();
            particles.SetActive(false);
        }
    }
}
