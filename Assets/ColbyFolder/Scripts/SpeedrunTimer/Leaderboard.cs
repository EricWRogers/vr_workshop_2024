using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    private static Leaderboard _instance;
    [HideInInspector]
    public bool summoningClone = false;
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
    [SerializeField]
    private Vector3 overlayPosition;
    [SerializeField]
    private Vector3 overlayScale;
    private TextMeshProUGUI leaderboard;
    private GameObject speedrunTimer;
    private List<KeyValuePairData> top5;
    private GameObject player;

    //Must be awake and not start because LoadLeaderboard is called in SaveManager's start
    private void Awake()
    {
        //Turns this object into a singleton
        if (_instance != null && _instance != this)
        {
            if (Instance.summoningClone)
            {
                Instance.summoningClone = false;
            }
            else
            {
                Destroy(gameObject);
            }
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
        if (_instance != this)
        {
            return;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        if (scene.name == "WinScene")
        {
            transform.GetChild(4).GetComponent<SaveManager>().Save();
            speedrunTimer.SetActive(false);
            leaderboard.transform.parent.gameObject.SetActive(true);
            transform.position = winSceneLeaderboardPosition;
            transform.rotation = winSceneLeaderboardRotation;
            GetComponent<RectTransform>().localScale = new Vector3(winSceneLeaderboardScale.x, winSceneLeaderboardScale.y, winSceneLeaderboardScale.z);
            GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        }
        else if (scene.name == "StarterAreaScene")
        {
            speedrunTimer.GetComponent<SpeedrunTimer>().ResetTimer();
            currentRunHighscore = false;
            speedrunTimer.SetActive(false);
            leaderboard.transform.parent.gameObject.SetActive(true);
            transform.position = starterAreaSceneLeaderboardPosition;
            transform.rotation = starterAreaSceneLeaderboardRotation;
            GetComponent<RectTransform>().localScale = new Vector3(starterAreaSceneLeaderboardScale.x, starterAreaSceneLeaderboardScale.y, starterAreaSceneLeaderboardScale.z);
            GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        }
        //Normal trial scene
        else
        {
            speedrunTimer.SetActive(true);
            speedrunTimer.GetComponent<SpeedrunTimer>().StartTimer();
            leaderboard.transform.parent.gameObject.SetActive(false);
            //In WASD this puts the speedrun timer as an overlay on the camera
            if (player.GetComponent<FPSController>())
            {
                GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            }
            else //You can't have overlay camera in VR
            {
                //Puts a speedrunTimer as a child of the VR camera to display it as an overlay
                summoningClone = true;
                GameObject newCanvas = Instantiate(gameObject, player.transform.GetChild(0).transform.GetChild(0));
                newCanvas.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
                newCanvas.transform.localPosition = new Vector3(overlayPosition.x, overlayPosition.y, overlayPosition.z);
                newCanvas.transform.localRotation = Quaternion.identity;
                newCanvas.transform.localScale = new Vector3(overlayScale.x, overlayScale.y, overlayScale.z);
            }
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
        top5.Add(new KeyValuePairData("Placeholder Name", speed));
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