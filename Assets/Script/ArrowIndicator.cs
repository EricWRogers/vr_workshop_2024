using UnityEngine;

public class ArrowIndicator : MonoBehaviour
{
    public GameObject indicatorPrefab; // Cube prefab
    public Material fireMaterial, iceMaterial, windMaterial, earthMaterial, normalMaterial; // Element materials
    private GameObject indicatorCube;
    private ArrowTypes arrowTypes; // Reference to ArrowTypes script
    private Transform bowTransform;

    void Start()
    {
        // Find ArrowTypes script
        arrowTypes = FindObjectOfType<ArrowTypes>();

        if (arrowTypes == null)
        {
            Debug.LogError("ArrowTypes script not found!");
            return;
        }

        // Try to find the bow after it spawns
        InvokeRepeating(nameof(FindBow), 0f, 0.5f); // Check every 0.5 seconds
    }

    void FindBow()
    {
        GameObject bow = GameObject.FindWithTag("Bow");
        if (bow != null)
        {
            bowTransform = bow.transform;
            SpawnIndicator();
            CancelInvoke(nameof(FindBow)); // Stop searching once found
        }
    }

    void SpawnIndicator()
    {
        // Make sure the bow exists before spawning the indicator
        if (bowTransform == null) return;

        // Spawn the indicator cube and parent it to the bow
        indicatorCube = Instantiate(indicatorPrefab, bowTransform);
        indicatorCube.transform.localPosition = new Vector3(0.2f, 0, 0); // Adjust position as needed

        UpdateIndicator();
    }

    void Update()
    {
        if (indicatorCube != null)
        {
            UpdateIndicator();
        }
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
