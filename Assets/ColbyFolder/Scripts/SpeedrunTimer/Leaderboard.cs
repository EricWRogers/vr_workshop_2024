using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    private static Leaderboard _instance;
    public static Leaderboard Instance { get { return _instance; } }
    public bool currentRunHighscore = false;
    [HideInInspector]
    public int currentRunIndex;
    [SerializeField]
    private Vector3 starterAreaSceneLeaderboardPosition;
    [SerializeField]
    private Quaternion starterAreaSceneLeaderboardRotation;
    [SerializeField]
    private Vector3 starterAreaSceneLeaderboardScale;
    [SerializeField]
    private Vector3 winSceneLeaderboardPosition;
    [SerializeField]
    private Quaternion winSceneLeaderboardRotation;
    [SerializeField]
    private Vector3 winSceneLeaderboardScale;
    private TextMeshProUGUI leaderboard;
    private GameObject speedrunTimer;
    private List<KeyValuePairData> top5;

    //Must be awake and not start because LoadLeaderboard is called in SaveManager's start
    private void Awake()
    {
        //Turns this object into a singleton
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //The leaderboard text is found 2 children down
        leaderboard = transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        speedrunTimer = transform.GetChild(1).gameObject;

        top5 = new List<KeyValuePairData>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //Called before Awake
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "WinScene")
        {
            speedrunTimer.SetActive(false);
            leaderboard.transform.parent.gameObject.SetActive(true);
            leaderboard.transform.position = winSceneLeaderboardPosition;
            leaderboard.transform.rotation = winSceneLeaderboardRotation;
            GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            //inputField.transform.position = spot for it to go to
        }
        else if (scene.name == "StarterAreaScene")
        {
            speedrunTimer.SetActive(false);
            leaderboard.transform.parent.gameObject.SetActive(true);
            leaderboard.transform.position = starterAreaSceneLeaderboardPosition;
            GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        }
        //Normal trial scene
        else
        {
            speedrunTimer.SetActive(true); //This line causes errors on scene load for some reason
            leaderboard.transform.parent.gameObject.SetActive(false);
            GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        }
    }

    public void ClearLeaderboard()
    {
        leaderboard.text = string.Empty;
        top5 = new List<KeyValuePairData>();
        LoadLeaderboard(top5);
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
            else
            {
                leaderboard.text = "No highscores";
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