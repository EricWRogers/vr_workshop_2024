using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class DoorScript : MonoBehaviour
{
    public GameObject particles;
    public float speed = 10f;
    private Vector3 startingPosition;
    public bool moving = true;

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
        transform.Translate(Vector3.down * step);
        if(moving)
        {
            AudioManager.instance.PlayOnObject("Stone_door", gameObject);
        }
        particles.SetActive(true);
    }


}
