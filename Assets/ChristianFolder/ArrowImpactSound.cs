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
        if (!other.gameObject.CompareTag("Bow") && !other.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlayOnObject("Stone_Impact", gameObject);
        }
    }
}
