using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterChecker : MonoBehaviour
{

    private GameObject player;
    public GameObject water;
    public float height = 17.0f;
    private bool aboveHeight = false;
    void Start()
    {
     player = GameObject.FindWithTag("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        aboveHeight = (player.transform.position.y >height) ? true : false;
        water.SetActive(aboveHeight);
    }
}
