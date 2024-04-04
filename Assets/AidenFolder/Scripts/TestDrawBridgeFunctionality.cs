using System.Collections.Generic;
using UnityEngine;

public class TestDrawBridgeFunctionality : MonoBehaviour
{
    public GameObject rope;
    public GameObject ropeTwo;
    public List<GameObject> brigdeStoppers;

    void Update()
    {
       if(rope == null && ropeTwo == null)
       {
            for(int i = 0; i < brigdeStoppers.Count; i++)
            {
                Destroy(brigdeStoppers[i]);
            }
       }
    }
}
