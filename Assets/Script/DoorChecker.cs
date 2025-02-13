using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorChecker : MonoBehaviour
{
    public bool doorClosed = false;
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("" +  other.name);
        if (other.tag == "DoorTop")
        {
            AudioManager.instance.Stop("Stone_door");
            AudioManager.instance.PlayOnObject("Stone_crash", gameObject);
            door.GetComponent<DoorScript>().speed = 0f;
            door.SetActive(false);
        }
    }
}
