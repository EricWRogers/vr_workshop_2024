using UnityEngine;

public class FirstTargets : MonoBehaviour
{
    public bool isHit = false;
    public FirstTargetChecker targetChecker;
    public Material hitMaterial;

    public void HitTarget()
    {
        if (!isHit)
        {
            isHit = true;
            targetChecker.CheckTargets();
            GetComponent<MeshRenderer>().material = hitMaterial;
        }
    }
}