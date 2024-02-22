using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatedTargetLogic : MonoBehaviour
{
    public UpdatedManager Manager;

    public float globalTime = 0.0f;
    public bool running = false;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            
            if (!running)
            {
                StartTimer();
            }


            Manager.hitTargets.Add(gameObject);
            //targetScript.CheckPuzzleCompletion();
        }
    }

    public void StartTimer()
    {
        running = true;
    }

    public void StopTimer()
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


            
        }
    }
}
