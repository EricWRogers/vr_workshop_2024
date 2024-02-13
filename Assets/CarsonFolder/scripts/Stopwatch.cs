using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class Stopwatch: MonoBehaviour
{
    public GlobalTimerManager timerManager;

    
    public List<GameObject> hitTargets = new List<GameObject>(); //list to store hit targets
    private const int totalTargets = 3;

    

    //cxheck if all targets are hit and update the complete flah
    public void CheckPuzzleCompletion()
    {
        if (hitTargets.Count == totalTargets)
        {
            timerManager.complete = true;
        }
       
        
    }

    // reset the puzzle if the timer runs out
    public void ResetPuzzle()
    {
        hitTargets.Clear();
        timerManager.complete = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the timer has run out and the puzzle is not complete
        if (!timerManager.complete && timerManager.running && timerManager.globalTime >= 10.0f)
        {
            // Reset the puzzle if the timer runs out and the puzzle is not complete
            ResetPuzzle();
            timerManager.StopGlobalTimer(); // Stop the global timer
        }
    }
}
