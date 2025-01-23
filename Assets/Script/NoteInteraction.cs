using UnityEngine;

public class NoteInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject ParchmentText;
    public GameObject NoteBackground;
    public bool InRange = false;

    public void Start()
    {
        ParchmentText.SetActive(false);
        NoteBackground.SetActive(false);
    }

    void Update()
    {
        if (InRange)
        {
            OpenText();
        }
        else
        {
            CloseText();
        }
    }

    public void OpenText()
    {
        ParchmentText.SetActive(true);
        NoteBackground.SetActive(true);
    }

    public void CloseText()
    {
        ParchmentText.SetActive(false);
        NoteBackground.SetActive(false);
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
            InRange = true;
        }
    }
    
    void OnTriggerExit(Collider other) 
    {
        InRange = false;
        ParchmentText.SetActive(false);
        NoteBackground.SetActive(false);
    }
}