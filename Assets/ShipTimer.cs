using SuperPupSystems.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTimer : MonoBehaviour
{
    public float totalTime;
    public List<Timer> timers; 


    public void StartShipTimers()
    {
        float timeDiff = totalTime / timers.Count;
        for (int i = 0; i < timers.Count; i++)
        {
            Timer timer = timers[i];
            totalTime = totalTime - timeDiff;
            timer.StartTimer(totalTime + timeDiff);
        }
    }
}
