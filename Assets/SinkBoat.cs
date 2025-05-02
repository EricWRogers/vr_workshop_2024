using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkBoat : MonoBehaviour
{
    private Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void SinkAnim()
    {
        m_Animator.SetBool("IsSinking", true);
    }
    public void DoneSinking()
    {
        gameObject.SetActive(false);
    }
}
