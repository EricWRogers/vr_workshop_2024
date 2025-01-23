using UnityEngine;

public class BridgeTargets : MonoBehaviour
{
    public GameObject rope;

    public void DestroyRope()
    {
        Destroy(rope);
        Destroy(gameObject);
    }
}