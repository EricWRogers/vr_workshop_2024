using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameObject player;
    private Quiver quiver;
    private Vector3 rightQuiverRotation = new Vector3(-60f, 90f, -90f);
    private Vector3 leftQuiverRotation = new Vector3(-120f, 90f, -90f);
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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (!rightHandMode)
        {
            ChangeToLeftHandMode();
        }
    }

    public void ChangeToRightHandMode()
    {
        VRInputs[] inputs = player.GetComponents<VRInputs>();
        //Change righthand bow spawn to left hand
        inputs[0].deviceCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.Left;
        //Swap what hand the bow spawns at
        (player.GetComponent<SpawnBow>().rightController, player.GetComponent<SpawnBow>().leftController) = (player.GetComponent<SpawnBow>().leftController, player.GetComponent<SpawnBow>().rightController);
        //Change arrow spawning from left hand to right hand
        inputs[1].deviceCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.Right;
        //Swap hands in SpawnArrowVR
        (player.GetComponent<SpawnArrowVR>().leftController, player.GetComponent<SpawnArrowVR>().rightController) = (player.GetComponent<SpawnArrowVR>().rightController, player.GetComponent<SpawnArrowVR>().leftController);
        //Access the quiver
        quiver = player.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Quiver>();
        //Swap hands for the quiver
        (quiver.leftController, quiver.rightController) = (quiver.rightController, quiver.leftController);
        //Flip the quiver model
        Transform quiverPouch = player.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform;
        quiverPouch.localEulerAngles = rightQuiverRotation;
        rightHandMode = true;
    }

    public void ChangeToLeftHandMode() 
    {
        VRInputs[] inputs = FindObjectOfType<SpawnArrowVR>().GetComponents<VRInputs>();
        //Change lefthand bow spawn to right hand
        inputs[0].deviceCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.Right;
        //Swap what hand the bow spawns at
        (player.GetComponent<SpawnBow>().rightController, player.GetComponent<SpawnBow>().leftController) = (player.GetComponent<SpawnBow>().leftController, player.GetComponent<SpawnBow>().rightController);
        //Change arrow spawning from right hand to left hand
        inputs[1].deviceCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.Left;
        //Swap hands in SpawnArrowVR
        (player.GetComponent<SpawnArrowVR>().leftController, player.GetComponent<SpawnArrowVR>().rightController) = (player.GetComponent<SpawnArrowVR>().rightController, player.GetComponent<SpawnArrowVR>().leftController);
        //Access the quiver
        quiver = player.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Quiver>();
        //Swap hands for the quiver
        (quiver.leftController, quiver.rightController) = (quiver.rightController, quiver.leftController);
        //Flip the quiver model
        Transform quiverPouch = player.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform;
        quiverPouch.localEulerAngles = leftQuiverRotation;
        rightHandMode = false;
    }
}