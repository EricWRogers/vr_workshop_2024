using UnityEngine;
using UnityEngine.Events;

public class TheTargetScript : MonoBehaviour
{
    public UnityEvent hitEvent;
    public UnityEvent unhitEvent;
    public bool isHit = false;
    public enum arrow_Types { Normal, Fire, Earth, Ice, Wind };
    public arrow_Types arrowRequired;
    public void Hit()
    {
        if(isHit == false)
        {
            isHit = true;
            hitEvent.Invoke();
        }
    }
    public void Unhit()
    {
        isHit = false;
        unhitEvent.Invoke();
    }
}
