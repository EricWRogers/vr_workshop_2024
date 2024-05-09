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
    private Light TargetGlow;
    public float MaxIntensity = 4.0f;
    public float MinIntensity = 0.0f;
    public float CurrentIntensity = 0.0f;
    private float IntensityChangeRate;

    private void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        startingMaterial = meshRenderer.material;
        TargetGlow = GetComponentInChildren<Light>();
        CurrentIntensity = MinIntensity;
        TargetGlow.intensity = CurrentIntensity;
        IntensityChangeRate = MaxIntensity / timerDuration;
    }

    public void StartPuzzleSolver()
    {
        if (!timerManager.puzzleComplete)
        {
            timer = timerDuration;
            meshRenderer.material = hitMaterial;
            timerManager.AddTargetToList(this);
            CurrentIntensity = MaxIntensity;
        }
    }

    private void Update()
    {
        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
            CurrentIntensity -= IntensityChangeRate * Time.deltaTime;
            CurrentIntensity = Mathf.Clamp(CurrentIntensity, MinIntensity, MaxIntensity);
            TargetGlow.intensity = CurrentIntensity;

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
            TargetGlow.intensity = 0.0f;
        }
    }
}
