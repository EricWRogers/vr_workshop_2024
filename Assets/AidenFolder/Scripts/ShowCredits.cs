using UnityEngine;

public class ShowCredits : MonoBehaviour
{
    public GameObject exitDoorWay;
    public GameObject startOverDoorWay;

    public GameObject creditText;
    public GameObject approachText;
    public GameObject whichWayToGoText;

    //public ParticleSystem particles;
    
    // Awake is called before the first frame update
    void Awake()
    {
        exitDoorWay.SetActive(false);
        startOverDoorWay.SetActive(false);
        creditText.SetActive(false);
        whichWayToGoText.SetActive(false);

        //particles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            approachText.SetActive(false);
            exitDoorWay.SetActive(true);
            startOverDoorWay.SetActive(true);
            creditText.SetActive(true);
            whichWayToGoText.SetActive(true);

            //particles.Play();
        }
    }
}
