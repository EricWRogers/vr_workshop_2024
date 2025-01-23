using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseHit : MonoBehaviour
{
    public Material hitMaterial;
    private Material baseMaterial;
    public float timeToStayHit = 0.5f;
    private float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        baseMaterial = GetComponent<MeshRenderer>().material;
    }

  

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            GetComponent<MeshRenderer>().material = baseMaterial;
        }
    }

    public void GotHit()
    {
        GetComponent<MeshRenderer>().material = hitMaterial;
        timer = timeToStayHit;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            GotHit();
        }
    }
}
