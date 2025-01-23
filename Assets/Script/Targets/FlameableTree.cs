using UnityEngine;

public class FlameableTree : MonoBehaviour
{
    public GameObject explosionVFX;
    public GameObject target;

    public void OnHit()
    {
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        target.SetActive(true);
        Destroy(gameObject);
    }
}