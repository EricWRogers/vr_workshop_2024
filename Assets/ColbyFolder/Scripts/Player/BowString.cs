using UnityEngine;

public class BowString : MonoBehaviour
{
    private Animator animator;
    private Vector3 localPosition;
    public FollowTransformOnRail joint;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        localPosition = transform.localPosition;
    }

    public void OnGrab()
    {
        joint.isGrabbing = true;
        //animator.Play("Take001");
    }

    public void OnStopGrab()
    {
        transform.localPosition = localPosition;
        joint.isGrabbing = false;
        //animator.StopPlayback();
    }
}