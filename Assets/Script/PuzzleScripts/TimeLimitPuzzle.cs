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
    private float timeLeft;
    public bool timerIsDone = false;
    public bool puzzleStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        numOfTargetsHit = 0;
        timeLeft = timeToComplete;
    }

    // Update is called once per frame
    void Update()
    {
        if(numOfTargetsHit > 0)
        {
            Timer();
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

    public void Timer()
    {
        puzzleStarted = true;
        Debug.Log("" + timeLeft);
        if(!timerIsDone && puzzleStarted)
            timeLeft -= Time.deltaTime;
        if (timeLeft <= 0.0f)
        {
            timerIsDone = true;
            RestPuzzle();
            TimeOut.Invoke();
        }
    }

    public void RestPuzzle()
    {
        puzzleStarted = false;
        timerIsDone = false;
        timeLeft = timeToComplete + Time.deltaTime;
        numOfTargetsHit = 0;
        for(int i = 0; i <= targets.Count; i++)
        {
            targets[i].Unhit();
        }
    }
}
