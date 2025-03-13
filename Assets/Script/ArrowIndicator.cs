using UnityEngine;

public class ArrowIndicator : MonoBehaviour
{
    public Transform bowTransform; // Reference to the bow
    public GameObject indicatorPrefab; // Cube prefab
    public Material fireMaterial, iceMaterial, windMaterial, earthMaterial, normalMaterial; // Element materials
    
    private GameObject indicatorCube;
    private ArrowTypes arrowTypes; // Reference to ArrowTypes script
    private MeshRenderer indicatorRenderer;

    void Start()
    {
        // Find the ArrowTypes script in the scene
        arrowTypes = FindObjectOfType<ArrowTypes>();

        if (arrowTypes == null)
        {
            Debug.LogError("ArrowTypes script not found!");
            return;
        }

        // Spawn the indicator cube
        indicatorCube = Instantiate(indicatorPrefab, bowTransform.position + bowTransform.right * 0.2f, Quaternion.identity, bowTransform);
        
        // Cache MeshRenderer for performance
        indicatorRenderer = indicatorCube.GetComponent<MeshRenderer>();

        UpdateIndicator();
    }

    void Update()
    {
        UpdateIndicator();
    }

    void UpdateIndicator()
    {
        if (indicatorCube == null || arrowTypes == null || indicatorRenderer == null) return;

        switch (arrowTypes.typesOfArrow)
        {
            case ArrowTypes.arrow_Types.Fire:
                indicatorRenderer.material = fireMaterial;
                break;
            case ArrowTypes.arrow_Types.Ice:
                indicatorRenderer.material = iceMaterial;
                break;
            case ArrowTypes.arrow_Types.Wind:
                indicatorRenderer.material = windMaterial;
                break;
            case ArrowTypes.arrow_Types.Earth:
                indicatorRenderer.material = earthMaterial;
                break;
            default:
                indicatorRenderer.material = normalMaterial;
                break;
        }
    }
}
