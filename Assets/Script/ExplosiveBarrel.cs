using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public RotateTurntable turnTable;
    public GameObject explosionVFX;
    [SerializeField] private float ExplosiveRange = 3f;
    [SerializeField] private LayerMask explodableLayerMask;

#if (UNITY_EDITOR)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosiveRange);
    }
#endif

    public void Detonate()
    {
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Collider[] objectsToExplode = Physics.OverlapSphere(transform.position, ExplosiveRange, explodableLayerMask);

        for(int i = 0; i < objectsToExplode.Length; i++)
        {
            
            if (objectsToExplode[i].tag == "Target" && !objectsToExplode[i].GetComponent<TheTargetScript>().isHit)
            {
                Debug.Log(objectsToExplode[i].name);
                objectsToExplode[i].GetComponent<TheTargetScript>().Hit();
            }
            //AudioManager.instance.PlayAtPosition("Explosion_sound", transform.position);
            objectsToExplode[i].gameObject.SetActive(false);

        }

    }
}
