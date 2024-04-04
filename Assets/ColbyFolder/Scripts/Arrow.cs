using UnityEngine;

public class Arrow : MonoBehaviour
{
    private GameObject player;
    private GameObject fireEffects;
    public float fireTimer = 0.0f;
    public bool arrowNocked = false;
    public float lengthOfFire = 8.0f;
    public bool onFire = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fireEffects = transform.GetChild(0).gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Bow"))
        {
            player.GetComponent<SpawnArrowVR>().arrowNocked = true;
            arrowNocked = true;
        }
        else if (other.CompareTag("FireZone"))
        {
            onFire = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bow"))
        {
            player.GetComponent<SpawnArrowVR>().arrowNocked = false;
            arrowNocked = false;
        }
        else if (other.CompareTag("FireZone") && player.GetComponent<SpawnArrowVR>().arrowNocked)
        {
            onFire = false;
        }
    }

    private void Update()
    {
        fireTimer -= Time.deltaTime;
        if (!arrowNocked && onFire && fireTimer < 0.0f)
        {
            onFire = false;
        }

        fireEffects.SetActive(onFire);
    }
}