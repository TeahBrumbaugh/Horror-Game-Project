using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] private string[] stageSceneNames;  // List of scene names for each puzzle stage
    private int currentStage = 0;
    private int attemptCount = 0;
    private const int maxAttempts = 3;

    public void GoToNextStage()
    {
        Debug.Log($"Correct! Advancing to stage {currentStage + 1}.");

        currentStage++;
        attemptCount = 0;

        if (currentStage < stageSceneNames.Length)
        {
            SceneManager.LoadScene(stageSceneNames[currentStage]);
        }
        else
        {
            Debug.Log(" All stages complete!");
            // Optionally load a win screen or loop
        }
    }

    public void FailAttempt()
    {
        attemptCount++;
        Debug.Log($" Incorrect. Attempt {attemptCount}/{maxAttempts}");

        if (attemptCount >= maxAttempts)
        {
            Debug.Log(" Too many failed attempts. Restarting from Stage 1.");
            currentStage = 0;
            attemptCount = 0;

            SceneManager.LoadScene(stageSceneNames[0]); // Reload Stage 1
        }
        else
        {
            Debug.Log(" Resetting current puzzle...");
            ResetCurrentPuzzle();  // Reset puzzle without reloading scene
        }
    }

    private void ResetCurrentPuzzle()
    {
        // Try to find a puzzle in the current scene that implements ResetPuzzle()
        var puzzle = FindObjectOfType<MonoBehaviour>() as IPuzzleResettable;

        if (puzzle != null)
        {
            puzzle.ResetPuzzle();
        }
        else
        {
            Debug.LogWarning(" No puzzle found that implements IPuzzleResettable. Consider reloading the scene.");
        }
    }

    // Optional: Accessors for external scripts
    public int GetStage() => currentStage;
    public int GetAttemptCount() => attemptCount;
}
