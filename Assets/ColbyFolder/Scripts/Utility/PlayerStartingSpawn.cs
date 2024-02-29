using UnityEngine;

public class PlayerStartingSpawn : MonoBehaviour
{
    public GameObject spawnPoints;
    public int spawnIndex;

    private void Start()
    {
        transform.position = spawnPoints.transform.GetChild(spawnIndex).transform.position;
    }
}