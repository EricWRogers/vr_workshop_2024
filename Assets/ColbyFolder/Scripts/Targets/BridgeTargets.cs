using UnityEngine;

public class BridgeTargets : MonoBehaviour
{
    public GameObject rope;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            Destroy(rope);
            Destroy(gameObject);
        }
    }
}