using UnityEngine;

public class RotateTurntable : MonoBehaviour
{
    public float rotateSpeed;
    public bool isPuzzleDone = false;
    
    void Update()
    {
        if(!isPuzzleDone)
        {
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }
    }
}
