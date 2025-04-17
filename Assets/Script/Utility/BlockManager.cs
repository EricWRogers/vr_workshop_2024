using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class BlockManager : MonoBehaviour
{

    private Queue<GameObject> earthWallQueue = new Queue<GameObject>();
    private Queue<GameObject> iceBlockQueue = new Queue<GameObject>();

    public int maxIceBlockAmt;
    public int maxEarthWallAmt;
    public static BlockManager instance;
    // Start is called before the first frame update

    void Awake()
    {
      if (instance == null){
        instance = this;
      }  
      else{
        Destroy(this);
        return;
      }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddIceBlock(GameObject _iceBlock){

        if(iceBlockQueue.Count >= maxIceBlockAmt){
            Destroy(iceBlockQueue.Dequeue());
        }
        iceBlockQueue.Enqueue(_iceBlock);
    }

    public void AddEarthWall(GameObject _earthWall){
        
        if(earthWallQueue.Count >= maxEarthWallAmt){
            Destroy(earthWallQueue.Dequeue());
        }

        earthWallQueue.Enqueue(_earthWall);
    }
}
