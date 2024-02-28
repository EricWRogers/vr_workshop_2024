using UnityEngine;

public class RopeCutSegment : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            Debug.Log("Hit by an arrow");
            Destroy(gameObject);
        }
    }
}