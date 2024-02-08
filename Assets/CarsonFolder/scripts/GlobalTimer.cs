using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTimerManager : MonoBehaviour
{
    public float globalTime = 0.0f;
    public bool running = false;

    public TargetScript targetScript; 

    void Start()
    {
        running = false;
    }

    public void StartGlobalTimer()
    {
        running = true;
    }

    public void StopGlobalTimer()
    {
        running = false;
    }

    void Update()
    {
        if (running)
        {
            globalTime += Time.deltaTime;
            int wholeNumber = (int)globalTime;

            int des = (int)((globalTime - wholeNumber) * 1000);

            
            if (targetScript.complete)
            {
                StopGlobalTimer();
            }
        }
    }
}
