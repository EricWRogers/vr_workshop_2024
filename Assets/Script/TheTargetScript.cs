using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TheTargetScript : MonoBehaviour
{
    private UnityEvent m_hitTarget;
    public bool needsFire;
    public void Hit()
    {
        m_hitTarget.Invoke();
    }
}
