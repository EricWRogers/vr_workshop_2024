using UnityEngine;

public class FireTargets : MonoBehaviour
{
    public bool isHit = false;
    public SecondTargetChecker targetChecker;

    public void OnHit()
    {
        if (!isHit)
        {
            isHit = true;
            transform.GetChild(0).gameObject.SetActive(true);
            targetChecker.CheckTargets();
        }
    }
}