using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string savePath = null;
    public SpeedrunTimer timer;
    private Leaderboard leaderboard;

    private void Start()
    {
        savePath = Application.persistentDataPath + "/save.save";
        leaderboard = transform.GetComponentInParent<Leaderboard>();
        Load();
    }

    public void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        leaderboard.TestSpeed(timer.accumulatedTime);

        Save save = new Save()
        {
            highscores = leaderboard.GetHighscores()
        };

        using (FileStream fileStream = File.Create(savePath))
        {
            binaryFormatter.Serialize(fileStream, save);
        }
    }

    public void Load()
    {
       //If there is a save file
       if (File.Exists(savePath))
       {
            Save save;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(savePath, FileMode.Open))
            {
                save = (Save)binaryFormatter.Deserialize(fileStream);
            }

            //Send the leaderboard the key/value pair from the save file
            leaderboard.LoadLeaderboard(save.highscores);
        }
        else
        {
            //Send the leaderboard a new key/value pair
            leaderboard.LoadLeaderboard(new List<KeyValuePairData>());
        }
    }

    public void WipeSave()
    {
        Debug.Log("Save Wiped");
        Save save = new Save();
        leaderboard.ClearLeaderboard();
    }
}
