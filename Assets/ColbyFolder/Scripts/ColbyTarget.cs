using UnityEngine;

public class ColbyTarget : MonoBehaviour
{
    public bool hit = false;
    public Material hitMaterial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            hit = true;
            GetComponent<MeshRenderer>().material = hitMaterial;
            GetComponent<SpawnThing>().SpawnTheThing();
        }
    }
}
