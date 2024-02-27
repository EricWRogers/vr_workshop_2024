using System.Collections.Generic;

[System.Serializable]
public class KeyValuePairData
{
    public string key;
    public double value;

    public KeyValuePairData(string key, double value)
    {
        this.key = key;
        this.value = value;
    }
}

[System.Serializable]
public class Save
{
    public List<KeyValuePairData> highscores;
}