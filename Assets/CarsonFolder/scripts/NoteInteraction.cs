using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject ParchmentText;
    public GameObject NoteBackground;
    public GameObject InteractText;
    public bool InRange = false;
    public bool Action = false;

    public void Start()
    { 
     InteractText.SetActive(false);
     ParchmentText.SetActive(false);
     NoteBackground.SetActive(false);
    
    }
    void Update()
    {
        if (InRange == true) 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
             InteractText.SetActive(false);

             ParchmentText.SetActive(true);  
             NoteBackground.SetActive(true);
             Action = true;
            }
        }
        if (Action == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractText.SetActive(true);

                ParchmentText.SetActive(false);
                NoteBackground.SetActive(false);
                Action = false;
            }
        }



    }


#if (UNITY_EDITOR)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(2, 1, 2));

    }
#endif

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
         InteractText.SetActive(true);
         InRange = true;
        }
    }
    
    void OnTriggerExit(Collider other) 
    {
     InteractText.SetActive(false);
     InRange = false;
     ParchmentText.SetActive(false);
     NoteBackground.SetActive(false);
    }
    
    
    
    
}
