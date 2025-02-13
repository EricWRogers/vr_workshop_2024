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
    public void Awake()
    {
        baseMaterial = this.GetComponent<Renderer>().material;
    }
    public void Hit()
    {
        if(isHit == false)
        {
            isHit = true;
            hitEvent.Invoke();
        }
    }
    public void Unhit()
    {
        isHit = false;
        this.GetComponent<Renderer>().material = baseMaterial;
        unhitEvent.Invoke();
    }

    public void ChangeMaterial()
    {
        this.GetComponent<Renderer>().material = hitMaterial;
    }
}
