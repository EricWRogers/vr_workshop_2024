using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowImpactSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //AudioManager.instance.PlayOnObject("Stone_Impact", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //AudioManager.instance.PlayOnObject("Fire_Sound", GetComponent<GameObject>());
        //OnTriggerEnter();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Target")){
            AudioManager.instance.PlayOnObject("Target_hit", gameObject);
        }

        if (!other.gameObject.CompareTag("Bow") && !other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Target") && !other.gameObject.CompareTag("Arrow"))
        {
            AudioManager.instance.PlayOnObject("Stone_Impact", gameObject);
        }
    }

   
}
