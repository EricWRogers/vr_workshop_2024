using UnityEngine;

public class TargetPractice : MonoBehaviour
{
    public Material hitMaterial;
    private Material baseMaterial;
    public float timeToStayHit = 5.0f;
    private float timer = 0.0f;

    private void Start()
    {
        baseMaterial = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            GetComponent<MeshRenderer>().material = baseMaterial;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            GetComponent<MeshRenderer>().material = hitMaterial;
            timer = timeToStayHit;
        }
    }
}