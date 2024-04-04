using UnityEngine;

public class FireTargets : MonoBehaviour
{
    public bool isHit = false;
    public SecondTargetChecker targetChecker;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            if (!isHit && other.GetComponent<Arrow>().onFire)
            {
                isHit = true;
                transform.GetChild(0).gameObject.SetActive(true);
                targetChecker.CheckTargets();
            }
        }
    }
}