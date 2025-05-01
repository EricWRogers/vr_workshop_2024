using UnityEngine;
using static ArrowTypes;

public class ArrowIndicator : MonoBehaviour
{
    public Material fireMaterial, iceMaterial, windMaterial, earthMaterial, normalMaterial; // Element materials
    private ArrowTypes arrowTypes; // Reference to ArrowTypes script
    private Transform bowTransform;


    void Start()
    {
        // Find ArrowTypes script
        arrowTypes = GameObject.FindGameObjectWithTag("Player").GetComponent<ArrowTypes>();

        if (arrowTypes == null)
        {
            Debug.LogError("ArrowTypes script not found!");
            return;
        }

    }

    void Update()
    {
        
        switch (arrowTypes.typesOfArrow)
        {
            case ArrowTypes.arrow_Types.Normal:
                Debug.Log("Normal Case");
                this.gameObject.GetComponent<Renderer>().material = normalMaterial;
                break;
            case ArrowTypes.arrow_Types.Fire:
                this.gameObject.GetComponent<Renderer>().material = fireMaterial;
                break;
            case ArrowTypes.arrow_Types.Ice:
                this.gameObject.GetComponent<Renderer>().material = iceMaterial;
                break;
            case ArrowTypes.arrow_Types.Earth:
                this.gameObject.GetComponent<Renderer>().material = earthMaterial;
                break;  
            default:
                break;
        }
        

    }

}
