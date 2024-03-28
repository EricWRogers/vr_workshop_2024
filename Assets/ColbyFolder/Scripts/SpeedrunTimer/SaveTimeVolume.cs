using UnityEngine;

public class SaveTimeVolume : MonoBehaviour
{
    private Leaderboard leaderboard;

    private void Start()
    {
        leaderboard = Leaderboard.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            leaderboard.transform.GetChild(4).GetComponent<SaveManager>().Save();
        }
    }
}