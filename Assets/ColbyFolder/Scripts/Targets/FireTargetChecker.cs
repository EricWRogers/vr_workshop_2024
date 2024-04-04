using UnityEngine;

public class FireTargetChecker : MonoBehaviour
{
    private int targetsHit = 0;
    private bool startMoving = false;
    public int totalTargets = 2;
    public GameObject doorToMove;
    public float heightOfDoorOpening = 10f;
    public float speed = 10f;
    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition = doorToMove.transform.position;
    }

    public void CheckTargets()
    {
        targetsHit++;
        if (targetsHit == totalTargets)
        {
            startMoving = true;
        }
    }

    void Update()
    {
        if (startMoving)
        {
            float step = speed * Time.deltaTime;
            doorToMove.transform.position = Vector3.MoveTowards(doorToMove.transform.position, startingPosition + new Vector3(0, heightOfDoorOpening, 0), step);
        }
    }
}