using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool rightHandMode = true;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}