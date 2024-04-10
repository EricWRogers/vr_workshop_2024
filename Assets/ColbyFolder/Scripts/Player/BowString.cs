using UnityEngine;

public class BowString : MonoBehaviour
{
    private Vector3 localPosition;
    public FollowTransformOnRail joint;

    private void Start()
    {
        localPosition = transform.localPosition;
    }

    public void OnGrab()
    {
        joint.isGrabbing = true;
    }

    public void OnStopGrab()
    {
        transform.localPosition = localPosition;
        joint.isGrabbing = false;
    }
}