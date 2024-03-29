using UnityEngine;

public class UpdatedTargetLogic : MonoBehaviour
{
    public float timerDuration = 10.0f; 
    private float timer = 0.0f;
    public MeshRenderer meshRenderer;
    public UpdatedManager timerManager;
    public Material hitMaterial; //To show feedback to the player they hit the target
    public Material winMaterial; //To show feedback to the player they won
    private Material startingMaterial; //To go back to once the timer runs out

    private void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        startingMaterial = meshRenderer.material;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow") && !timerManager.puzzleComplete)
        {
            //Invoke("CompleteTimer", timerDuration); Let's try not to use CoRoutines. Generally bad deals
            timer = timerDuration;
            meshRenderer.material = hitMaterial;
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
        if (!timerManager.puzzleComplete) 
        {
            timerManager.RemoveTargetFromList(this);
            meshRenderer.material = startingMaterial;
        }
    }
}
