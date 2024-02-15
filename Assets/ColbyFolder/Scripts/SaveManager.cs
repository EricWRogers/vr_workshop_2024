using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using TMPro;

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
        Save save = new Save()
        {
            savedTime = timer.accumulatedTime
        };

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

            leaderboard.text = "Name: " + save.savedTime.ToString();
        }
        else
        {
            leaderboard.text = "No Highscores";
        }
    }
}
