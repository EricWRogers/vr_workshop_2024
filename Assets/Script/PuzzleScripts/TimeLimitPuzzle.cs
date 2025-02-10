using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TimeLimitPuzzle : MonoBehaviour
{
    public UnityEvent SolvedPuzzled;
    public UnityEvent TimeOut;
    public List<TheTargetScript> targets;
    public int numOfTargetsHit;
    public float timeToComplete;
    public bool timerIsDone = false;
    // Start is called before the first frame update
    void Start()
    {
        numOfTargetsHit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(numOfTargetsHit > 0)
        {
            Timer(timeToComplete);
        }
        


        if (numOfTargetsHit >= targets.Count)
        {
            SolvedPuzzled.Invoke();
        }
    }
    public void AddPoint()
    {
        numOfTargetsHit++;
    }

    public void Timer(float _time)
    {
        float timeLeft = _time;
        if(!timerIsDone)
            timeToComplete -= Time.deltaTime;
        if (timeToComplete <= 0.0f)
        {
            timerIsDone = true;
            RestPuzzle();
            TimeOut.Invoke();
        }
    }

    public void RestPuzzle()
    {
        numOfTargetsHit = 0;
        for(int i = 0; i <= targets.Count; i++)
        {
            targets[i].Unhit();
        }
    }
}
