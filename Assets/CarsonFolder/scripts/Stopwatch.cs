using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    public float time = 0.0f;
    public bool running = false;
    //public TMP_Text TimerUI;
    public bool ishit = false;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ishit)
        {
            running = true;
        }
        if (running)
        {
            time += Time.deltaTime;
            int wholeNumber = (int)time;

            int des = (int)((time - wholeNumber) * 1000);


            //TimerUI.text = time.ToString();
        }   
    }
    public void Stop() 
    {
        running = false;
    }



}
