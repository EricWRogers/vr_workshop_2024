using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirVent : MonoBehaviour
{
    public float liftForce = 10f;
    public float duration = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                StartCoroutine(ApplyForce(rb));
            }
        }
    }

    private System.Collections.IEnumerator ApplyForce(Rigidbody playerRb)
    {
        float timer = 0f;
        while (timer < duration)
        {
            playerRb.AddForce(Vector3.up * liftForce, ForceMode.Acceleration);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
