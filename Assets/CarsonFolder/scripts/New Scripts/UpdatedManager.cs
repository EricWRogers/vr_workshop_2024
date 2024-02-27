using UnityEngine;
using System.Collections.Generic;

public class UpdatedManager: MonoBehaviour
{
    public List<UpdatedTargetLogic> targets = new List<UpdatedTargetLogic>();
    public int targetsToCompletePuzzle = 3;
    public bool puzzleComplete = false;
    public AudioSource waa;

    void Start()
    {
        
        puzzleComplete = false;
    }

    public void AddTargetToList(UpdatedTargetLogic target)
    {
        
        targets.Add(target);
        CheckPuzzleCompletion();
    }

    public void RemoveTargetFromList(UpdatedTargetLogic target)
    {
        
        targets.Remove(target);
        CheckPuzzleCompletion();
    }

    void CheckPuzzleCompletion()
    {
        
        if (targets.Count == targetsToCompletePuzzle)
        {
          
            puzzleComplete = true;
         
            Debug.Log("yippee");
            //waa.Play();
        }
        else
        {
            
            puzzleComplete = false;
        }
    }
}
