using UnityEngine;

public class FreezeDrawBridge : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DrawBridge"))
        {
            AudioManager.instance.PlayAtPosition("Stone_crash", transform.position);
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
