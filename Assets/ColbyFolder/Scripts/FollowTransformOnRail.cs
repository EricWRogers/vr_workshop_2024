using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// snaps our transform to the target trans, position but restricts position to be 0,0 x,y and between min and max on z when FireOnRail is fired.
/// Is reset to init pos when ResetPosition is fired.
/// Script was made to keep a bowsting only moving in one axis between 2 positions. Could be used to make slider.
/// </summary>
public class FollowTransformOnRail : MonoBehaviour
{
    public Transform targetTransform;
    XRBaseController rightController;
    XRBaseController leftController;
    private Animator bowAnimator;
    public bool isGrabbing = false;
    
    public float railMin = -0.7f;
    public float railMax = 0;

    public float pullAmount = 0.0f;

    Vector3 _resetPosition;

    void Start()
    {
        _resetPosition = targetTransform.localPosition;
        bowAnimator = GetComponentInParent<Animator>();
        rightController = GameManager.Instance.rightController.GetComponent<XRBaseController>();
        leftController = GameManager.Instance.leftController.GetComponent<XRBaseController>();
    }

    private void Update()
    {
        transform.position = targetTransform.position;
        transform.localPosition = new Vector3(_resetPosition.x, _resetPosition.y, Mathf.Clamp(transform.localPosition.z, railMin + _resetPosition.z, railMax + _resetPosition.z));
        CalculatePullAmount();
    }

    public void ResetPosition()
    {
        transform.localPosition = _resetPosition;
        GetComponents<AudioSource>()[1].Play();
        leftController.SendHapticImpulse(0.3f, 0.02f);
        rightController.SendHapticImpulse(0.6f, 0.02f);
    }

    public void CalculatePullAmount()
    {
        float previousPullAmount = pullAmount; // Store the previous pull amount
        if (!isGrabbing)
        {
            bowAnimator.SetFloat("PullBack", 0);
            // Check if the bowstring was being pulled back in the previous frame
            if (previousPullAmount > 0)
            {
                // If so, stop the sound effect
                GetComponents<AudioSource>()[0].Stop();
            }
        }
        else
        {
            pullAmount = Vector3.Distance(_resetPosition, transform.localPosition) / Mathf.Abs(railMin);
            pullAmount = Mathf.Clamp(pullAmount, 0.0f, 1.0f);
            bowAnimator.SetFloat("PullBack", pullAmount);

            // If the pull amount has increased (bowstring being pulled back)
            if (pullAmount > previousPullAmount)
            {
                // Adjust pitch based on pull amount
                GetComponents<AudioSource>()[0].pitch = pullAmount + 1;
                // If the audio is not already playing, start playing it
                if (!GetComponent<AudioSource>().isPlaying)
                {
                    GetComponents<AudioSource>()[0].Play();
                }
            }
        }
    }

}
