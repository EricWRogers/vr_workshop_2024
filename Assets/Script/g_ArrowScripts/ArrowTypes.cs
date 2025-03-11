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
        /*if (typesOfArrow == ArrowTypes.arrow_Types.Normal)
        {
            typesOfArrow = ArrowTypes.arrow_Types.Fire;
        }
        else if (typesOfArrow == ArrowTypes.arrow_Types.Fire)
        {
            typesOfArrow = ArrowTypes.arrow_Types.Normal;
        }*/
        Debug.Log("Switched Arrow Type to: " + typesOfArrow);
    }
    // Start is called before the first frame update
        public void SwitchToNextArrow()
    {
        // Cycle to the next arrow type
        typesOfArrow = (arrow_Types)(((int)typesOfArrow + 1) % System.Enum.GetValues(typeof(arrow_Types)).Length);
        Debug.Log("Switched Arrow Type to: " + typesOfArrow);
    }

    public void SwitchToPreviousArrow()
    {
        // Cycle to the previous arrow type (looping around)
        int newIndex = (int)typesOfArrow - 1;
        if (newIndex < 0) newIndex = System.Enum.GetValues(typeof(arrow_Types)).Length - 1;
        typesOfArrow = (arrow_Types)newIndex;
        Debug.Log("Switched Arrow Type to: " + typesOfArrow);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
