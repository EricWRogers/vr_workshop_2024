//just deletes the object attached so it only appears in the visual editor, not any play mode.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEditorOnly : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
