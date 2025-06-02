using System.Collections;
using NUnit.Framework.Constraints;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] private string stageSceneName;  // Fill this in the Inspector
    [SerializeField] private GameObject jumpScarePanel;
    private int currentStage = 0;
    private int attemptCount = 0;
    [SerializeField] private int maxAttempts = 3;
    [SerializeField] private Animator animator;

    public void GoToNextStage()
    {
        Debug.Log($"Correct!.");

        attemptCount = 0;

        if (stageSceneName != "WIN")
        {
            SceneManager.LoadScene(stageSceneName);
        }
        else
        {
            Debug.Log("All stages complete!");
            SceneManager.LoadScene("WIN");
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
            StartCoroutine(JumpScareAndRestart());
        }
    }

    public IEnumerator JumpScareAndRestart()
    {

        jumpScarePanel.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("OfficialLevel1");
    }

    public int GetStage() => currentStage;
    public int GetAttemptCount() => attemptCount;

    private IEnumerator FadeInAndOut()
    {
        if (animator != null)
            animator.SetTrigger("Start");
        yield return new WaitForSeconds(1f);

    }
}
