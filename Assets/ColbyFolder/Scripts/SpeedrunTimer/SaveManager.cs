using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using TMPro;
using System;

public class SaveManager : MonoBehaviour
{
    private string savePath = null;
    public SpeedrunTimer timer;
    public TextMeshProUGUI leaderboard;

    private void Start()
    {
        savePath = Application.persistentDataPath + "/save.save";
        Load();
    }

    public void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        /*
        string tempLeaderboard = "";

        tempLeaderboard = leaderboard.text;
        */
        Save save = new Save()
        {
            savedTime = timer.accumulatedTime
        };
        /*

        int minutes = (int)savedTime / 60;
        int seconds = (int)savedTime % 60;
        double milliseconds = (savedTime - minutes*60 - seconds)*100;
        leaderboard.text = "Name: " + string.Format("{0:##0}:{1:00}.{2:00}", minutes, seconds, milliseconds);
        */

        using (FileStream fileStream = File.Create(savePath))
        {
            binaryFormatter.Serialize(fileStream, save);
        }
    }

    public void Load()
    {
       if (File.Exists(savePath))
       {
            Save save;

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(savePath, FileMode.Open))
            {
                save = (Save)binaryFormatter.Deserialize(fileStream);
            }

            leaderboard.text = "Name: " + TimeSpan.FromSeconds(save.savedTime).ToString(@"mm\:ss\:ff");
        }
        else
        {
            leaderboard.text = "No Highscores";
        }
    }
}
