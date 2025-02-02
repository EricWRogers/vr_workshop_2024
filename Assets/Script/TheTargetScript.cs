using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TheTargetScript : MonoBehaviour
{
    public UnityEvent m_hitTarget;
    public enum arrow_Types { Normal, Fire, Earth, Ice, Wind };
    public arrow_Types arrowRequired;
    public void Hit()
    {
        m_hitTarget.Invoke();
    }
}
