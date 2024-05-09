using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public RotateTurntable turnTable;
    public GameObject explosionVFX;
    public SecondTargetChecker targetChecker;
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
        turnTable.isPuzzleDone = true;
        targetChecker.CheckTargets();
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Collider[] objectsToExplode = Physics.OverlapSphere(transform.position, ExplosiveRange, explodableLayerMask);

        foreach(var objectToExplode in objectsToExplode) 
        {
            Destroy(objectToExplode.gameObject);
            AudioManager.instance.PlayOnObject("Explosion_sound", gameObject);
        }
    }
}
