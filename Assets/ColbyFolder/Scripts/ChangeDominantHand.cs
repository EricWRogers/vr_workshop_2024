using UnityEngine;

public class ChangeDominantHand : MonoBehaviour
{
    private bool rightHandMode = true;
    private VRInputs[] inputs;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (rightHandMode)
            {
                rightHandMode = false;
                inputs = other.GetComponents<VRInputs>();
                //Change lefthand bow spawn to right hand
                inputs[0].deviceCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.Right;
                //Swap what hand the bow spawns at
                (other.GetComponent<SpawnBow>().rightController, other.GetComponent<SpawnBow>().leftController) = (other.GetComponent<SpawnBow>().leftController, other.GetComponent<SpawnBow>().rightController);
            }
            else
            {
                rightHandMode = true;
                //Change righthand bow spawn to left hand
                inputs[0].deviceCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.Left;
                //Swap what hand the bow spawns at
                (other.GetComponent<SpawnBow>().rightController, other.GetComponent<SpawnBow>().leftController) = (other.GetComponent<SpawnBow>().leftController, other.GetComponent<SpawnBow>().rightController);
            }
        }
    }
}