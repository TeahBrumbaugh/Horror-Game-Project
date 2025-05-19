using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] private string[] stageSceneNames;  // Fill this in the Inspector
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
            Debug.Log("All stages complete!");
            // Optionally load a win screen or loop
        }
    }

    public void FailAttempt()
    {
        attemptCount++;
        Debug.Log($"Incorrect. Attempt {attemptCount}/{maxAttempts}");

        if (attemptCount >= maxAttempts)
        {
            Debug.Log("Too many failed attempts. Restarting from Puzzle 1.");
            currentStage = 0;
            attemptCount = 0;
            SceneManager.LoadScene(stageSceneNames[0]);
        }
        else
        {
            // Optionally show retry feedback
        }
    }

    // Optional accessors
    public int GetStage() => currentStage;
    public int GetAttemptCount() => attemptCount;
}
