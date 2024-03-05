using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
   private float ExplosiveRange = 3f;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosiveRange);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            Detonate();
        }

    }

    public void Detonate()
    {
        Collider[] objectsToExplode = Physics.OverlapSphere(transform.position, ExplosiveRange);

        foreach(var objectToExplode in objectsToExplode) 
        {
            Destroy(objectToExplode.gameObject);
        }



    }
}
