using UnityEngine;

public class FlameableTree : MonoBehaviour
{
    public GameObject explosionVFX;
    public GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            if (other.GetComponent<Arrow>().onFire)
            {
                Instantiate(explosionVFX, transform.position, Quaternion.identity);
                target.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}