using UnityEngine;

public class ArrowImpactSound : MonoBehaviour
{
    private bool soundPlayed = false;
    void OnCollisionEnter(Collision other)
    {
        if (soundPlayed)
        {
            return;
        }
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
        soundPlayed = true;
    }
}
