using UnityEngine;

public class ArrowImpactSound : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            AudioManager.instance.Play("Target_hit");
        }
        else if(other.gameObject.CompareTag("Water"))
        {
            AudioManager.instance.PlayOnObject("Water_splash", gameObject);
        }
        else
        {
            AudioManager.instance.PlayOnObject("Stone_Impact", gameObject);
        }
    }
}
