using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { get; private set; }
    public Transform puzzleContainer;

    // Keep one dictionary of prefab -> instance
    Dictionary<GameObject, GameObject> _instances = new Dictionary<GameObject, GameObject>();
    GameObject _currentPuzzle;
    GameObject _currentPrefab;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void OpenPuzzle(GameObject prefab, MonoBehaviour[] disableOnOpen, LockCursor lockCursor)
    {
        // disable player/input
        if (disableOnOpen!=null)
            foreach (var mb in disableOnOpen) mb.enabled = false;
        if (lockCursor != null) lockCursor.SetCursorState(false);

        // hide whatever was up before
        if (_currentPuzzle != null)
            _currentPuzzle.SetActive(false);

        // get or create this puzzle’s instance
        if (!_instances.TryGetValue(prefab, out var instance))
        {
            instance = Instantiate(prefab, puzzleContainer);
            _instances[prefab] = instance;
            var puzzle = instance.GetComponent<PuzzleBase>();
            if (puzzle == null)
            {
                Debug.LogError($"Prefab {prefab.name} has no PuzzleBase!");
            }
            else puzzle.Show();
        }

        // show it
        instance.SetActive(true);
        _currentPuzzle = instance;
        _currentPrefab = prefab;
    }

    public void CloseCurrentPuzzle()
    {
        if (_currentPuzzle != null)
            _currentPuzzle.SetActive(false);

        // re-enable player/input
        var all = FindObjectsByType<Interactable3D>(FindObjectsSortMode.None);
        foreach (var interact in all)
            if (interact.disableOnOpen != null)
                foreach (var mb in interact.disableOnOpen)
                    mb.enabled = true;

        if (all.Length > 0 && all[0].cameraLockController != null)
            all[0].cameraLockController.SetCursorState(true);

        _currentPuzzle = null;
        _currentPrefab = null;
    }

    public bool IsPuzzleOpen() => _currentPuzzle != null && _currentPuzzle.activeSelf;
}
