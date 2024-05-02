using UnityEngine;

public class ChangeDominantHand : MonoBehaviour
{
    private bool rightHandMode = true;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (rightHandMode)
            {
                rightHandMode = false;
                GameObject temp = other.GetComponent<SpawnBow>().leftController;
                other.GetComponent<SpawnBow>().leftController = other.GetComponent<SpawnBow>().rightController;
                other.GetComponent<SpawnBow>().rightController = temp;
            }
            else
            {
                rightHandMode = true;
                GameObject temp = other.GetComponent<SpawnBow>().leftController;
                other.GetComponent<SpawnBow>().leftController = other.GetComponent<SpawnBow>().rightController;
                other.GetComponent<SpawnBow>().rightController = temp;
            }
        }
    }
}