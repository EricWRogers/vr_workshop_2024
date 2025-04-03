using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCrystal : MonoBehaviour
{
    public GameObject teleportPos;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TeleportToCrystal()
    {
        Player.transform.position = teleportPos.transform.position;
    }
}
