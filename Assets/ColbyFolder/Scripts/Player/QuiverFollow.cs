using UnityEngine;

public class QuiverFollow : MonoBehaviour
{
    public Transform playerOrigin;
    public Transform playerCam;
    public float quiverX = 0f;
    public float quiverY = -0.8f;
    public float quiverZ = 0f;

    private void Update()
    {
        transform.position = new Vector3(playerOrigin.position.x + playerCam.position.x, playerOrigin.position.y + playerCam.position.y, playerOrigin.position.z + playerCam.position.z);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.y + playerCam.rotation.eulerAngles.y - 90, 0f);
    }
}