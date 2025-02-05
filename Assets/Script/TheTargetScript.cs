using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TheTargetScript : MonoBehaviour
{
    public UnityEvent hitTarget;
    public bool isHit = false;
    public enum arrow_Types { Normal, Fire, Earth, Ice, Wind };
    public arrow_Types arrowRequired;
    public void Hit()
    {
        if(isHit == false)
        {
            isHit = true;
            hitTarget.Invoke();
        }
    }
}
