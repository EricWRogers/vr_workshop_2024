using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class TargetScript : MonoBehaviour
{
    public GlobalTimerManager timerManager;

    public bool complete = false; //flag to check if the puzzle is complete
    private List<GameObject> hitTargets = new List<GameObject>(); //list to store hit targets
    private const int totalTargets = 3;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            // start the global timer when the first target is hit
            if (!timerManager.running)
            {
                timerManager.StartGlobalTimer();
            }

            
            hitTargets.Add(gameObject);
            CheckPuzzleCompletion();
        }
    }

    //cxheck if all targets are hit and update the complete flag
    void CheckPuzzleCompletion()
    {
        complete = hitTargets.Count == totalTargets;
    }

    // reset the puzzle if the timer runs out
    public void ResetPuzzle()
    {
        hitTargets.Clear();
        complete = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the timer has run out and the puzzle is not complete
        if (!complete && timerManager.running && timerManager.globalTime >= 10.0f)
        {
            // Reset the puzzle if the timer runs out and the puzzle is not complete
            ResetPuzzle();
            timerManager.StopGlobalTimer(); // Stop the global timer
        }
    }
}
