using UnityEngine;

public class PlayerStartingSpawn : MonoBehaviour
{
    public GameObject spawnPoints;
    public int spawnIndex;

    private void Start()
    {
        if (spawnPoints != null)
        {
            transform.position = spawnPoints.transform.GetChild(spawnIndex).transform.position;
        }
    }
}