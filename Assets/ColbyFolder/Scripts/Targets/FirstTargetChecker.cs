using UnityEngine;

public class FirstTargetChecker : MonoBehaviour
{
    private int targetsHit = 0;
    public int totalTargets = 3;
    public GameObject bridgeToActivate;

    public void CheckTargets()
    {
        targetsHit++;
        if (targetsHit == totalTargets)
        {
            bridgeToActivate.SetActive(true);
        }
    }
}