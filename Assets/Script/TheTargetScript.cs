using UnityEngine;
using UnityEngine.Events;

public class TheTargetScript : MonoBehaviour
{
    public UnityEvent hitEvent;
    public UnityEvent unhitEvent;
    public bool isHit = false;
    public Material hitMaterial;
    private Material baseMaterial;
    public enum arrow_Types { Normal, Fire, Earth, Ice, Wind };
    public arrow_Types arrowRequired;
    public float timeToComplete;
    private float timeLeft;
    private bool timerStart;
    [Tooltip("Sets if Target has a timer")]
    public bool hasTimer;

    [SerializeField]
    private TimeLimitPuzzle timepuzzle;
    public void Awake()
    {
        baseMaterial = this.GetComponent<Renderer>().material;

    }

    public void Update()
    {
        if(hasTimer){

            if (timeLeft>=0){
                timeLeft -= Time.deltaTime;
            }
            if (timerStart && timeLeft<=0){
                Unhit();
            }
        }

        
    }
    public void Hit()
    {
        if(isHit == false)
        {
            isHit = true;
            hitEvent.Invoke();

        }
        timerStart = true;
        timeLeft = timeToComplete;
    }
    public void Unhit()
    {
        isHit = false;
        this.GetComponent<Renderer>().material = baseMaterial;
        unhitEvent.Invoke();
        timerStart = false;
    }

    public void ChangeMaterial()
    {
        this.GetComponent<Renderer>().material = hitMaterial;
    }
}
