using UnityEngine;

public class SpawnThing : MonoBehaviour
{
    public GameObject thingToSpawn;
    [Tooltip("Make this an empty game object and move it to where you want the thing to spawn")]
    public GameObject spawnPosition;

    public void SpawnTheThing()
    {
        Instantiate(thingToSpawn, spawnPosition.transform.position, spawnPosition.transform.rotation);
    }
}
