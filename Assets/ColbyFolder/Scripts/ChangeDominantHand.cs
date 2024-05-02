using UnityEngine;

public class ChangeDominantHand : MonoBehaviour
{
    private bool rightHandMode = true;
    private VRInputs[] inputs;
    private Quiver quiver;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (rightHandMode)
            {
                inputs = other.GetComponents<VRInputs>();
                //Change lefthand bow spawn to right hand
                inputs[0].deviceCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.Right;
                //Swap what hand the bow spawns at
                (other.GetComponent<SpawnBow>().rightController, other.GetComponent<SpawnBow>().leftController) = (other.GetComponent<SpawnBow>().leftController, other.GetComponent<SpawnBow>().rightController);
                //Change arrow spawning from right hand to left hand
                inputs[1].deviceCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.Left;
                //Swap hands in SpawnArrowVR
                (other.GetComponent<SpawnArrowVR>().leftController, other.GetComponent<SpawnArrowVR>().rightController) = (other.GetComponent<SpawnArrowVR>().rightController, other.GetComponent<SpawnArrowVR>().leftController);
                //Access the quiver
                quiver = other.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Quiver>();
                //Swap hands for the quiver
                (quiver.leftController, quiver.rightController) = (quiver.rightController, quiver.leftController);
                rightHandMode = false;
            }
            else
            {
                inputs = other.GetComponents<VRInputs>();
                //Change righthand bow spawn to left hand
                inputs[0].deviceCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.Left;
                //Swap what hand the bow spawns at
                (other.GetComponent<SpawnBow>().rightController, other.GetComponent<SpawnBow>().leftController) = (other.GetComponent<SpawnBow>().leftController, other.GetComponent<SpawnBow>().rightController);
                //Change arrow spawning from left hand to right hand
                inputs[1].deviceCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.Right;
                //Swap hands in SpawnArrowVR
                (other.GetComponent<SpawnArrowVR>().leftController, other.GetComponent<SpawnArrowVR>().rightController) = (other.GetComponent<SpawnArrowVR>().rightController, other.GetComponent<SpawnArrowVR>().leftController);
                //Access the quiver
                quiver = other.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Quiver>();
                //Swap hands for the quiver
                (quiver.leftController, quiver.rightController) = (quiver.rightController, quiver.leftController);
                rightHandMode = true;
            }
        }
    }
}