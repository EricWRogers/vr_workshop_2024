using TMPro;
using UnityEngine;

public class SpeedrunTimer : MonoBehaviour
{
    public float accumulatedTime = 0f;
    private bool timerStarted = false;
    private TextMeshProUGUI timer = null;

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
            timer.text = accumulatedTime.ToString();
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
        timer.text = accumulatedTime.ToString();
    }

    public void LoadTimer(float savedTime)
    {
        accumulatedTime = savedTime;
        timer.text = accumulatedTime.ToString();
    }
}
