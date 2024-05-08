using UnityEngine;
using System.Collections.Generic;

public class UpdatedManager: MonoBehaviour
{
    public List<UpdatedTargetLogic> targets;
    public int targetsToCompletePuzzle = 3;
    public bool puzzleComplete = false;
    public AudioSource waa;

    public GameObject doorToCredits;

    void Awake()
    {
        doorToCredits.SetActive(false);
    }

    void Start()
    {
        puzzleComplete = false;

    }

    public void AddTargetToList(UpdatedTargetLogic target)
    {
        if (!targets.Contains(target))
        {
            targets.Add(target);
        }
        CheckPuzzleCompletion();
        CreateExitDoor();
    }

    public void RemoveTargetFromList(UpdatedTargetLogic target)
    {
        targets.Remove(target);
        CheckPuzzleCompletion();
        CreateExitDoor();
    }

    void CheckPuzzleCompletion()
    {
        if (targets.Count == targetsToCompletePuzzle)
        {
            puzzleComplete = true;
            foreach (UpdatedTargetLogic target in targets)
            {
                target.meshRenderer.material = target.winMaterial;
            }
         
            Debug.Log("yippee");
            //waa.Play();
        }
        else
        {
            puzzleComplete = false;
        }
    }

    void CreateExitDoor()
    {
        if(puzzleComplete == true)
        {
            doorToCredits.SetActive(true);
            AudioManager.instance.Play("Win_sound");
        }
    }
}
