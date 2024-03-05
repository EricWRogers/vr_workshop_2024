using TMPro;
using UnityEngine;

public class SpeedrunTimer : MonoBehaviour
{
    private bool timerStarted = false;
    public double accumulatedTime = 0.0f;
    private TextMeshProUGUI timer = null;
    [Tooltip("Multipiler for how fast the timer counts up, should be 1 for the build")]
    public double timerMultiplier = 1.0f;

    private void Start()
    {
        timer = GetComponent<TextMeshProUGUI>();
        StartTimer();
    }

    private void Update()
    {
        if (timer != null && timerStarted)
        {
            accumulatedTime += Time.deltaTime * timerMultiplier;
            int minutes = (int)accumulatedTime / 60;
            int seconds = (int)accumulatedTime % 60;
            double milliseconds = (accumulatedTime - minutes*60 - seconds)*100;
            timer.text = string.Format("{0:##0}:{1:00}.{2:00}", minutes, seconds, milliseconds);
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
        timer.text = "0:00.00";
    }

    public void LoadTimer(float savedTime)
    {
        accumulatedTime = savedTime;
    }
}
