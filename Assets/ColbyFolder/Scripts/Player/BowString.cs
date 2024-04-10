using UnityEngine;

public class BowString : MonoBehaviour
{
    private Animator animator;
    private Vector3 localPosition;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        localPosition = transform.localPosition;
    }

    public void OnGrab()
    {
        //animator.Play("Take001");
    }

    public void OnStopGrab()
    {
        transform.localPosition = localPosition;
        //animator.StopPlayback();
    }
}