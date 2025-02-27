using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LyreMusic : MonoBehaviour
{
    public string musicNote;


    void OnTriggerEnter(Collider other)
    {
        AudioManager.instance.PlayAtPosition(musicNote, transform.position);
    }
}
