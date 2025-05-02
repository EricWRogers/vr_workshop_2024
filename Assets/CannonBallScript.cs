using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CannonBallScript : MonoBehaviour
{
    public GameObject explosionVFX;
    [SerializeField] private float ExplosiveRange = 3f;
    [SerializeField] private LayerMask explodableLayerMask;
    // Start is called before the first frame update
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosiveRange);
    }
    private void OnTriggerEnter(Collider other)
    {
        Explode();
    }
    public void Explode()
    {
        Debug.Log("explode");
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Collider[] objectsToExplode = Physics.OverlapSphere(transform.position, ExplosiveRange, explodableLayerMask);

        for (int i = 0; i < objectsToExplode.Length; i++)
        {

            if (objectsToExplode[i].tag == "Player")
            {
                Debug.Log(objectsToExplode[i].name);
                SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
            }
        }

            AudioManager.instance.PlayAtPosition("Explosion_sound", transform.position);
        Destroy(gameObject);
    }
}
