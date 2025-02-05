using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultiTargetPuzzle : MonoBehaviour
{
    public UnityEvent SolvedPuzzled;
    public List<TheTargetScript> targets;
    public int numOfTargetsHit;
    // Start is called before the first frame update
    void Start()
    {
        numOfTargetsHit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(numOfTargetsHit >= targets.Count)
        {
            SolvedPuzzled.Invoke();
        }
    }
    public void AddPoint()
    {
        numOfTargetsHit++;
    }
}
