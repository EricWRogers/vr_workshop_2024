using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EarthBlock : MonoBehaviour
{
    // Start is called before the first frame update
 public Vector3 maxSize = new Vector3(3f, 3f, 3f); // Final size of the ice block
    public float growSpeed = 2f; // Growth speed multiplier
    private Vector3 initialSize;
    private bool isGrowing = false;
    public LayerMask groundLayer;

    [SerializeField]
    private List<RaycastHit> rayHits;

    public Collider[] hits;
    public RaycastHit[] raycastArray;
    public bool hasHit = false;

    void Start()
    {
        initialSize = Vector3.zero;
    }

    public void StartGrowing()
    {
        if (!isGrowing)
        {
            StartCoroutine(GrowEarthBlock());
            
        }
    }

    private void OnDrawGizmos()
    {
      Gizmos.color = Color.red;
      Gizmos.DrawWireCube(transform.position, transform.localScale);  
    }

    IEnumerator GrowEarthBlock()
    {
        isGrowing = true;
        float elapsedTime = 0f;
        float offset = transform.position.y + (maxSize.y / 2);
        transform.position = new Vector3 (transform.position.x, offset, transform.position.y);
        while (elapsedTime < 1f)
        {
            transform.localScale = Vector3.Lerp(initialSize, maxSize, elapsedTime);
            elapsedTime += Time.deltaTime * growSpeed;
            


            hits = Physics.OverlapBox(transform.position, transform.localScale, transform.rotation, groundLayer);

            
            

          raycastArray = Physics.BoxCastAll(transform.position, transform.localScale*0.5f , transform.forward, transform.rotation, 10000, ~groundLayer);
            foreach (RaycastHit _i in raycastArray){
                if(_i.collider.gameObject != gameObject){
                    
                    
                    hasHit = true;
                    Debug.Log(_i.collider.gameObject.name);  
                    yield break;
                }
            }
            
            yield return null;
        }


    }
}
