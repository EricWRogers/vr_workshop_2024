using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class ObjectPositionTracker
{
    private static GameObject selectedObject;
    private static Vector3 initialPosition;

    static ObjectPositionTracker()
    {
        // Register to listen to selection events in the Unity Editor
        Selection.selectionChanged += OnSelectionChanged;
        SceneView.duringSceneGui += DuringSceneGUI;
    }

    private static void OnSelectionChanged()
    {
        // This method is called whenever the selection changes in the Unity Editor

        // Check if there is a selected GameObject
        if (Selection.activeGameObject != null)
        {
            // Access the selected GameObject
            selectedObject = Selection.activeGameObject;
        }
        else
        {
            // Clear the selected object when nothing is selected
            selectedObject = null;
        }
    }

    private static void DuringSceneGUI(SceneView sceneView)
    {
        if (selectedObject != null)
        {
            if (selectedObject.GetComponent<PreventOverlap>())
            {
                Event e = Event.current;

                if (e.type == EventType.MouseDown)
                {
                    // MouseDown event indicates the start of dragging
                    initialPosition = selectedObject.transform.position;
                }
                else if (e.type == EventType.MouseUp)
                {
                    // MouseUp event indicates that the GameObject has been released
                    HandleRelease();
                }
            }
        }
    }

    private static void HandleRelease()
    {
        if (selectedObject == null)
            return;

        while (CheckForOverlaps(selectedObject))
        {
            selectedObject.transform.position = Vector3.MoveTowards(selectedObject.transform.position, initialPosition, 0.00001f);

            // If the object is close to the initial position, break
            if (Vector3.Distance(selectedObject.transform.position, initialPosition) < 0.0002f)
            {
                break;
            }
        }
    }

    private static bool CheckForOverlaps(GameObject myobject)
    {
        MeshRenderer meshRenderer = myobject.GetComponent<MeshRenderer>();
        Collider myCollider = myobject.GetComponent<Collider>();

        if (meshRenderer == null || myCollider == null)
        {
            return false;
        }

        Bounds boundsToCheck = meshRenderer.bounds;

        //Find all colliders around the object you are moving
        foreach (Collider collider in Physics.OverlapBox(boundsToCheck.center, boundsToCheck.extents))
        {
            GameObject otherObject = collider.gameObject;

            if (otherObject != myobject)
            {
                return Physics.ComputePenetration(
                myCollider,
                myCollider.transform.position,
                myCollider.transform.rotation,
                collider,
                collider.transform.position,
                collider.transform.rotation,
                out Vector3 _, out float _);
            }
        }

        return false; // No overlap detected
    }
}