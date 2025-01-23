using TMPro;
using UnityEngine;

public class FakeTimer : MonoBehaviour
{
    private void Update()
    {
        GetComponent<TextMeshProUGUI>().text = Leaderboard.Instance.speedrunTimer.GetComponent<SpeedrunTimer>().timer.text;
    }
}