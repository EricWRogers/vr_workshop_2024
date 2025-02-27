using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.ProBuilder.Shapes;

public class turret : MonoBehaviour
{

    public Transform attackpoint;
    public GameObject prefab;
    public float shotDelay = 1;
    private float time = 0;
    
    public bool isRotating = false;
    public float rotatingRange = 0;
    public float rotatingSpeed = 50;
  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time/shotDelay > 1.0f){
            GameObject clone =Instantiate(prefab, attackpoint.position, attackpoint.rotation);
            
            time = 0.0f;
        }

        if (isRotating)
        {
            attackpoint.rotation = Quaternion.Euler(attackpoint.rotation.x, Mathf.PingPong(Time.time * rotatingSpeed, rotatingRange*2)-rotatingRange, attackpoint.rotation.z);
        }
    }
}
