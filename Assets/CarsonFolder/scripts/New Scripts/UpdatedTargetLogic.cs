using UnityEngine;

public class UpdatedTargetLogic : MonoBehaviour
{
    public float timerDuration = 10.0f; 
    public UpdatedManager timerManager; 
    public bool isHit = false; 

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow") && !isHit)
        {
            
            Invoke("CompleteTimer", timerDuration);
            
            timerManager.AddTargetToList(this);
            
            isHit = true;
        }
    }

    
    void CompleteTimer()
    {
        
        timerManager.RemoveTargetFromList(this);
    }
}
