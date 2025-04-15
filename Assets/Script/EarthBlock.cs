using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBlock : MonoBehaviour
{
    // Start is called before the first frame update
 public Vector3 maxSize = new Vector3(3f, 3f, 3f); // Final size of the ice block
    public float growSpeed = 2f; // Growth speed multiplier
    private Vector3 initialSize;
    private bool isGrowing = false;
    public LayerMask groundLayer;

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

    IEnumerator GrowEarthBlock()
    {
        isGrowing = true;
        float elapsedTime = 0f;
        RaycastHit _hit;
        bool shouldBreak = false;
  

        while (elapsedTime < 1f)
        {
            transform.localScale = Vector3.Lerp(initialSize, maxSize, elapsedTime);
            elapsedTime += Time.deltaTime * growSpeed;
            yield return null;

        RaycastHit[] hits = Physics.BoxCastAll(transform.position, transform.localScale , transform.forward, transform.rotation, 10000, groundLayer);
        foreach (RaycastHit _i in hits){
            if (_i.point == transform.position){
                shouldBreak = true;
                break;

            }

        }
            if(shouldBreak){
                break;
            }

        }

        transform.localScale = maxSize; // Ensure it reaches the final size
    }
}
