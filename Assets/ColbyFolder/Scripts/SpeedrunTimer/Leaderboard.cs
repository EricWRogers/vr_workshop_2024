using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard Instance;
    public bool currentRunHighscore = false;
    [HideInInspector]
    public int currentRunIndex;
    private TextMeshProUGUI leaderboard;
    private List<KeyValuePairData> top5;

    //Must be awake and not start because LoadLeaderboard is called in SaveManager's start
    private void Awake()
    {
        //Turns this object into a singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        //The leaderboard text is found 2 children down
        leaderboard = transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        top5 = new List<KeyValuePairData>();
    }

    public void ClearLeaderboard()
    {
        leaderboard.text = string.Empty;
        top5 = new List<KeyValuePairData>();
    }

    public void LoadLeaderboard(List<KeyValuePairData> highscores)
    {
        if (highscores != null)
        {
            if (highscores.Count > 0)
            {
                //Sort by lowest value (ascending order)
                highscores.Sort((x, y) => x.value.CompareTo(y.value));

                top5 = highscores;
                ContructLeaderboardText(highscores);
            }
        }
        else
        {
            leaderboard.text = "No highscores";
        }
    }

    public void SetHighscoreName(string name)
    {
        top5[currentRunIndex].key = name;
        ContructLeaderboardText(top5);
    }

    public void TestSpeed(double speed)
    {
        //Add in the new value
        top5.Add(new KeyValuePairData("Unnamed", speed));
        //Sort the new value in
        top5.Sort((x, y) => x.value.CompareTo(y.value));
        for (int i = 0; i < top5.Count; i++)
        {
            if (top5[i].value == speed)
            {
                currentRunIndex = i;
            }
        }
        //Remove the slowest time if there are more than 5 entries
        if (top5.Count > 5)
        {
            //If the bottom score isn't the current speed, this run is a highscore
            if (currentRunIndex < 5)
            {
                currentRunHighscore = true;
            }
            top5.RemoveAt(top5.Count - 1);
        }
        //If there are less than 5 scores, this run is a highscore
        else
        {
            currentRunHighscore = true;
        }

        ContructLeaderboardText(top5);
    }

    private void ContructLeaderboardText(List<KeyValuePairData> highscores)
    {
        string leaderboardText = "";
        foreach (var kvp in highscores)
        {
            int minutes = (int)kvp.value / 60;
            int seconds = (int)kvp.value % 60;
            double milliseconds = (kvp.value - minutes * 60 - seconds) * 100;

            leaderboardText += kvp.key + ": " + string.Format("{0:00}:{1:00}.{2:00}\n", minutes, seconds, milliseconds);
        }

        leaderboard.text = leaderboardText;
    }

    public List<KeyValuePairData> GetHighscores()
    {
        return top5;
    }
}