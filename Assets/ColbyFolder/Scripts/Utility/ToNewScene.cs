using UnityEngine;
using UnityEngine.SceneManagement;

public class ToNewScene : MonoBehaviour
{
    public string sceneName;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.GatherAllSounds();
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}