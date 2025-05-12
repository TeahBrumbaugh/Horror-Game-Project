using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { get; private set; }
    [Tooltip("The PuzzleContainer")]
    public Transform puzzleContainer;

    GameObject _currentPuzzle;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    // Spawns the UI prefab under puzzleContainer, disables the listed MonoBehaviours, and unlocks the cursor.
    public void OpenPuzzle(GameObject prefab, MonoBehaviour[] disableOnOpen, LockCursor lockCursor)
    {
        foreach (var mb in disableOnOpen) mb.enabled = false;
        if (lockCursor != null) lockCursor.SetCursorState(false);

        _currentPuzzle = Instantiate(prefab, puzzleContainer);
        var puzzle = _currentPuzzle.GetComponent<PuzzleBase>();
        puzzle.Show();
    }


    // Destroys the current panel, re-enables all disabled scripts, and locks cursor.

    public void CloseCurrentPuzzle()
    {
        if (_currentPuzzle != null) Destroy(_currentPuzzle);

        // Re-enable everything
        var all = FindObjectsByType<Interactable3D>(FindObjectsSortMode.None);

        foreach (var interact in all)
            foreach (var mb in interact.disableOnOpen)
                mb.enabled = true;
        if (all.Length > 0 && all[0].cameraLockController != null)
            all[0].cameraLockController.SetCursorState(true);
    }

    public bool IsPuzzleOpen()
    {
        return _currentPuzzle != null;
    }

}

