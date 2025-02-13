using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTypes : MonoBehaviour
{

    public enum arrow_Types { Normal, Fire, Earth, Ice, Wind };
    public arrow_Types typesOfArrow = arrow_Types.Normal;

        public void SwitchArrowType()
    {
        // Cycle through enum values
        typesOfArrow = (arrow_Types)(((int)typesOfArrow + 1) % System.Enum.GetValues(typeof(arrow_Types)).Length);
        Debug.Log("Switched Arrow Type to: " + typesOfArrow);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
