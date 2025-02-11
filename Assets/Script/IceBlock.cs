using System.Collections;
using UnityEngine;

public class IceBlock : MonoBehaviour
{
    public Vector3 maxSize = new Vector3(3f, 3f, 3f); // Final size of the ice block
    public float growSpeed = 2f; // Growth speed multiplier
    private Vector3 initialSize;
    private bool isGrowing = false;

    void Start()
    {
        initialSize = Vector3.zero;
        transform.localScale = initialSize;
    }

    public void StartGrowing()
    {
        if (!isGrowing)
        {
            StartCoroutine(GrowIceBlock());
        }
    }

    IEnumerator GrowIceBlock()
    {
        isGrowing = true;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            transform.localScale = Vector3.Lerp(initialSize, maxSize, elapsedTime);
            elapsedTime += Time.deltaTime * growSpeed;
            yield return null;
        }

        transform.localScale = maxSize; // Ensure it reaches the final size
    }
}
