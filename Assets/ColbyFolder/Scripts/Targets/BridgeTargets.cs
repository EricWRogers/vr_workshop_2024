using UnityEngine;

public class BridgeTargets : MonoBehaviour
{
    public GameObject rope;

    public void DestroyRope()
    {
        Destroy(rope);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            Destroy(rope);
            Destroy(gameObject);
        }
    }
}