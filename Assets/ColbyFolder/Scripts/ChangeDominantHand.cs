using UnityEngine;

public class ChangeDominantHand : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.rightHandMode)
            {
                GameManager.Instance.ChangeToLeftHandMode();
            }
            else
            {
                GameManager.Instance.ChangeToRightHandMode();
            }
        }
    }
}