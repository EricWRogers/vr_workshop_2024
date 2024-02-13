using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetChecker : MonoBehaviour
{
    public Stopwatch targetScript;
    public GlobalTimerManager timerManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            // start the global timer when the first target is hit
            if (!timerManager.running)
            {
                timerManager.StartGlobalTimer();
            }


            targetScript.hitTargets.Add(gameObject);
            targetScript.CheckPuzzleCompletion();
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
