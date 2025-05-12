using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PuzzleInteractable : MonoBehaviour
{
    [Tooltip("Assign the puzzle UI prefab")]
    public GameObject puzzlePrefab;

    [Tooltip("Assign scripts to disable during the puzzle")]
    public MonoBehaviour[] disableOnPuzzleOpen;

    [Tooltip("Reference to LockCursor component")]
    public LockCursor cameraLockController;

    public void Interact()
    {
        if (puzzlePrefab == null)
        {
            Debug.LogError($"PuzzleInteractable on {name} has no puzzlePrefab assigned.");
            return;
        }

        PuzzleManager.Instance.OpenPuzzle(puzzlePrefab, disableOnPuzzleOpen, cameraLockController);
    }
}
