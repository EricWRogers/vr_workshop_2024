using SuperPupSystems.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterMover : MonoBehaviour
{
    public Timer timer;
    private Vector3 m_startPos;
    private Vector3 m_endPos;
    public float distance;
    public float m_time = 0;
    public float timeBetween;
    public bool m_goingUp = true;
    private bool shouldStop = false;
    // Start is called before the first frame update
    void Start()
    {
        m_startPos = this.transform.position;
        m_endPos = new Vector3(this.transform.position.x, this.transform.position.y + distance, this.transform.position.z) ;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(m_startPos, m_endPos, m_time / timeBetween);

        if (!shouldStop)
        {
            if (!m_goingUp)
            {
                m_time -= Time.deltaTime;
            }

            if (m_goingUp)
            {
                m_time += Time.deltaTime;
            }
        }   

    }
    public void ChangeDirction()
    {
        Debug.Log("1");
        m_goingUp = !m_goingUp;
    }

    public void OnTriggerEnter(Collider other)
    {
        shouldStop = true;
    }
}
