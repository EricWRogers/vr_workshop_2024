using UnityEngine;

public class ArrowIndicator : MonoBehaviour
{
    public Transform bowTransform; // Reference to the bow
    public GameObject indicatorPrefab; // Cube prefab
    public Material fireMaterial, iceMaterial, windMaterial, earthMaterial, normalMaterial; // Element materials
    private GameObject indicatorCube;
    private ArrowTypes arrowTypes; // Reference to ArrowTypes script

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
        UpdateIndicator();
    }

    void Update()
    {
        UpdateIndicator();
    }

    void UpdateIndicator()
    {
        if (indicatorCube == null || arrowTypes == null) return;

        switch (arrowTypes.typesOfArrow)
        {
            case ArrowTypes.arrow_Types.Fire:
                indicatorCube.GetComponent<MeshRenderer>().material = fireMaterial;
                break;
            case ArrowTypes.arrow_Types.Ice:
                indicatorCube.GetComponent<MeshRenderer>().material = iceMaterial;
                break;
            case ArrowTypes.arrow_Types.Wind:
                indicatorCube.GetComponent<MeshRenderer>().material = windMaterial;
                break;
            case ArrowTypes.arrow_Types.Earth:
                indicatorCube.GetComponent<MeshRenderer>().material = earthMaterial;
                break;
            default:
                indicatorCube.GetComponent<MeshRenderer>().material = normalMaterial;
                break;
        }
    }
}