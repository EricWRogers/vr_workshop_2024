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
    public bool TextUp = false;

    public void Start()
    {
        InteractText.SetActive(false);
        ParchmentText.SetActive(false);
        NoteBackground.SetActive(false);
    }
    void Update()
    {
        if (TextUp) 
        {
            InteractText.SetActive(false);
            ParchmentText.SetActive(true);
            NoteBackground.SetActive(true);
        }
        if (!TextUp) 
        {
            ParchmentText.SetActive(false);
            NoteBackground.SetActive(false);
        }
        
        
        
        if (Input.GetKeyDown(KeyCode.E)) 
        {
         Action = true;
        }
        if (Input.GetKeyUp(KeyCode.E)) 
        {
         Action = false;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TextUp = true;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            TextUp = false;
        }



        if (InRange && Action) 
        {
         TextUp = true;
        }
        if (!InRange && !Action) 
        {
         TextUp = false;
        }


















            /*if (TextUp == false) 
            {
             ParchmentText.SetActive(false);
             NoteBackground.SetActive(false);
            }
            if TextUp == true)
            {
             InteractText.SetActive(false);
             ParchmentText.SetActive(true);
             NoteBackground.SetActive(true);
            }
            if (InRange == true && Action == false) 
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                 Action = true;
                    Debug.Log("yippee");
                }
            }
            if (InRange == true && Action == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                 Action = false;
                }
            }
            if (InRange == false && Action == false)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Action = false;
                }
            }
            */
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
     Action = false;
    }
    
    
    
    
}
