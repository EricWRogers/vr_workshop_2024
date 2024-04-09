using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayOnObject("Fire_Sound", GetComponent<GameObject>());
    }

    // Update is called once per frame
    void Update()
    {
        //AudioManager.instance.PlayOnObject("Fire_sound_loop", GetComponent<GameObject>());
    }
}
