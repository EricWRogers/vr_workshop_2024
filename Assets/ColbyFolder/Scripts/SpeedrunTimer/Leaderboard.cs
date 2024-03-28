using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard Instance;
    public bool currentRunHighscore = false;
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

                //Construct the leaderboard text
                string leaderboardText = "";
                foreach (var kvp in highscores)
                {
                    //Add the key and value into the larger scoped variable
                    top5.Add(new KeyValuePairData(kvp.key, kvp.value)); 

                    int minutes = (int)kvp.value / 60;
                    int seconds = (int)kvp.value % 60;
                    double milliseconds = (kvp.value - minutes * 60 - seconds) * 100;

                    leaderboardText += kvp.key + ": " + string.Format("{0:00}:{1:00}.{2:00}\n", minutes, seconds, milliseconds);
                }

                leaderboard.text = leaderboardText;
            }
        }
        else
        {
            leaderboard.text = "No highscores";
        }
    }

    public void TestSpeed(double speed)
    {
        //Add in the new value
        top5.Add(new KeyValuePairData("Unnamed", speed));
        //Sort the new value in
        top5.Sort((x, y) => x.value.CompareTo(y.value));
        //Remove the slowest time if there are more than 5 entries
        if (top5.Count > 5)
        {
            //If the bottom score isn't the current speed, this run is a highscore
            if (top5[top5.Count - 1].value != speed)
            {
                currentRunHighscore = true;
                Debug.Log("Highscore!");
            }
            top5.RemoveAt(top5.Count - 1);
        }
        //If there are less than 5 scores, this run is a highscore
        else
        {
            currentRunHighscore = true;
            Debug.Log("Highscore!");
        }

        // Construct the leaderboard text
        string leaderboardText = "";
        foreach (var kvp in top5)
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

    public bool GetCurrentRunHighscore()
    {
        return currentRunHighscore;
    }

    public void SetCurrentRunHighscore(bool isHighscore)
    {
        currentRunHighscore = isHighscore;
    }
}
