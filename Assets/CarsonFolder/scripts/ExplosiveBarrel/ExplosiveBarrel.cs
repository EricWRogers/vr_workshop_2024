using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public RotateTurntable turnTable;
    [SerializeField] private float ExplosiveRange = 3f;
    [SerializeField] private LayerMask explodableLayerMask;

#if (UNITY_EDITOR)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosiveRange);

    }
#endif

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            Detonate();
            turnTable.isPuzzleDone = true;
        }

    }

    public void Detonate()
    {
        Collider[] objectsToExplode = Physics.OverlapSphere(transform.position, ExplosiveRange, explodableLayerMask);

        foreach(var objectToExplode in objectsToExplode) 
        {
            Destroy(objectToExplode.gameObject);
        }



    }
}
