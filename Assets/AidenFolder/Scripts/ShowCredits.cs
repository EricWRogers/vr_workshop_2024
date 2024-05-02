using UnityEngine;

public class ShowCredits : MonoBehaviour
{
    public GameObject exitDoorWay;
    public GameObject creditText;
    public GameObject approachText;
    public GameObject whichWayToGoText;
    public GameObject door1Model;
    public GameObject door2Model;

    //public ParticleSystem particles;
    
    // Awake is called before the first frame update
    void Awake()
    {
        exitDoorWay.SetActive(false);
        creditText.SetActive(false);
        whichWayToGoText.SetActive(false);
        door1Model.SetActive(true);
        door2Model.SetActive(true);
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
            creditText.SetActive(true);
            whichWayToGoText.SetActive(true);
            door1Model.SetActive(false);
            door2Model.SetActive(false);
            //particles.Play();
        }
    }
}
