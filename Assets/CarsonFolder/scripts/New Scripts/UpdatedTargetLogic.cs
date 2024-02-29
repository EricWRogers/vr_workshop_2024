using UnityEngine;

public class UpdatedTargetLogic : MonoBehaviour
{
    public float timerDuration = 10.0f; 
    private float timer = 0.0f;
    public UpdatedManager timerManager;
    public Material hitMaterial; //To show feedback to the player they hit the target
    private Material startingMaterial; //To go back to once the timer runs out

    private void Start()
    {
        startingMaterial = GetComponent<MeshRenderer>().material;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            //Invoke("CompleteTimer", timerDuration); Let's try not to use CoRoutines. Generally bad deals
            timer = timerDuration;
            timerManager.AddTargetToList(this);
            GetComponent<MeshRenderer>().material = hitMaterial;
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
        GetComponent<MeshRenderer>().material = startingMaterial;
    }
}
