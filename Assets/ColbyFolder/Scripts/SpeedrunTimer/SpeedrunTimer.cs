using System;
using System.Text;
using TMPro;
using UnityEngine;

public class SpeedrunTimer : MonoBehaviour
{
    public float accumulatedTime = 0f;
    private bool timerStarted = false;
    private TextMeshProUGUI timer = null;

    static StringBuilder timerStringBuilder = new StringBuilder(10); 

    private void Start()
    {
        timer = GetComponent<TextMeshProUGUI>();
        StartTimer();
    }

    private void Update()
    {
        if (timer != null && timerStarted)
        {
            accumulatedTime += Time.deltaTime;
            FormatTimer(TimeSpan.FromMilliseconds(accumulatedTime));
        }
    }

    public void StartTimer()
    {
        timerStarted = true;
    }

    public void PauseTimer()
    {
        timerStarted = false;
    }

    public void ResetTimer()
    {
        timerStarted = false;
        accumulatedTime = 0f;
        timer.text = "000:00.00";
    }

    public void LoadTimer(float savedTime)
    {
        accumulatedTime = savedTime;
        FormatTimer(TimeSpan.FromMilliseconds(accumulatedTime));
    }

    private void FormatTimer(TimeSpan time)
    {
        timerStringBuilder.Clear(); // Clear the StringBuilder from previous content

        int minutes = time.Minutes;
        int seconds = time.Seconds;
        int milliseconds = time.Milliseconds;

        // Append formatted time to StringBuilder
        timerStringBuilder.Append(minutes.ToString("D3")).Append(':')
            .Append(seconds.ToString("00")).Append('.')
            .Append((milliseconds / 10).ToString("00"));

        timer.text = timerStringBuilder.ToString();
    }
}
