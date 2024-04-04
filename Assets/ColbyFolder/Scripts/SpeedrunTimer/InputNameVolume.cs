using UnityEngine;
using UnityEngine.EventSystems;

public class InputNameVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Turn off player movement
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }
}