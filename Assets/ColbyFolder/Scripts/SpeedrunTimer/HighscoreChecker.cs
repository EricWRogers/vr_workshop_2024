using UnityEngine;

public class HighscoreChecker : MonoBehaviour
{
    Leaderboard leaderboard;

    private void Start()
    {
        leaderboard = Leaderboard.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (leaderboard.currentRunHighscore)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}