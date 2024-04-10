using UnityEngine;

/// <summary>
/// snaps our transform to the target trans, position but restricts position to be 0,0 x,y and between min and max on z when FireOnRail is fired.
/// Is reset to init pos when ResetPosition is fired.
/// Script was made to keep a bowsting only moving in one axis between 2 positions. Could be used to make slider.
/// </summary>
public class FollowTransformOnRail : MonoBehaviour
{

    public Transform targetTransform;
    public bool isGrabbing = false;
    
    public float railMin = -0.7f;
    public float railMax = 0;

    Vector3 _resetPosition;

    void Start()
    {
        _resetPosition = targetTransform.localPosition;
    }

    private void Update()
    {
        transform.position = targetTransform.position;
        transform.localPosition = new Vector3(_resetPosition.x, _resetPosition.y, Mathf.Clamp(transform.localPosition.z, railMin + _resetPosition.z, railMax + _resetPosition.z));
        float test = CalculatePullAmount();
        Debug.Log(test);
    }

    public void ResetPosition()
    {
        transform.localPosition = _resetPosition;
    }

    public float CalculatePullAmount()
    {
        if (!isGrabbing)
        {
            return 0;
        }
        float pullAmount = Vector3.Distance(_resetPosition, transform.localPosition) / Mathf.Abs(railMin);
        pullAmount = Mathf.Clamp(pullAmount, 0.0f, 1.0f);
        return pullAmount;
    }
}
