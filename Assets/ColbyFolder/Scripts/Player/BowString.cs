using UnityEngine;

public class BowString : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void OnGrab()
    {
        animator.Play("Take001");
    }

    public void OnStopGrab()
    {
        animator.StopPlayback();
    }
}