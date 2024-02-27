using UnityEngine;

public class UpdatedTargetLogic : MonoBehaviour
{
    public float timerDuration = 10.0f; 
    private float timer = 0.0f;
    public UpdatedManager timerManager; 

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            //Invoke("CompleteTimer", timerDuration); Let's try not to use CoRoutines. Generally bad deals
            timer = timerDuration;
            
            timerManager.AddTargetToList(this);
        }
    }

    private void Update()
    {
        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                CompleteTimer(); //This is a function call, not a CoRoutine
            }
        }
    }
    
    void CompleteTimer()
    {
        timerManager.RemoveTargetFromList(this);
    }
}
