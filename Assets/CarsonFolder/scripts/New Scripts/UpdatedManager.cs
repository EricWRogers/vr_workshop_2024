using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatedManager : MonoBehaviour
{
    public List<GameObject> hitTargets = new List<GameObject>(); //list to store hit targets
    private const int totalTargets = 3;
    public bool complete = false;


    public void CheckPuzzleCompletion()
    {
        if (hitTargets.Count == totalTargets)
        {
            complete = true;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
        if (complete) 
        { 
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
