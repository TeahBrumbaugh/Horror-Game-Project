using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DelayedAnswerChecker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_InputField inputField;        // Player's answer input
    [SerializeField] private MonoBehaviour puzzleScript;       // The puzzle (must implement IAnswerProvider)
    [SerializeField] private Button submitButton;              // Button to submit
    [SerializeField] private StageManager stageManager;        // Reference to StageManager

    [Header("Settings")]
    [SerializeField] private float delay = 2f;                 // Delay before grading

    private IAnswerProvider answerProvider;
    private bool isChecking = false;

    private void Awake()
    {
        // Try to get the answer provider interface from the puzzle script
        answerProvider = puzzleScript as IAnswerProvider;

        if (answerProvider == null)
        {
            Debug.LogError("Assigned puzzleScript does not implement IAnswerProvider.");
            enabled = false;
            return;
        }

        if (submitButton != null)
        {
            submitButton.onClick.AddListener(() => StartCoroutine(GradeAfterDelay()));
        }
    }

    private IEnumerator GradeAfterDelay()
    {
        if (isChecking) yield break;
        isChecking = true;

        string submitted = inputField.text.Trim();
        string correct = answerProvider.GetCorrectAnswer().Trim();

        inputField.interactable = false;
        submitButton.interactable = false;

        Debug.Log($"[Grader] Submitted: \"{submitted}\" — waiting {delay} second(s)...");

        yield return new WaitForSeconds(delay);

        if (submitted.Equals(correct, System.StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("[Grader]  Correct answer!");
            stageManager?.GoToNextStage();
        }
        else
        {
            Debug.Log($"[Grader]  Incorrect. Expected: \"{correct}\"");
            if (puzzleScript is WordleManager wordle)
            {
                Debug.Log("[Grader] Calling UpdateCurrentRow()...");
                wordle.UpdateCurrentRow(submitted);
            }
            stageManager?.FailAttempt();
        }

        isChecking = false;
        ResetAfterCheck();
    }

    private void ResetAfterCheck()
    {
        inputField.text = "";
        inputField.interactable = true;
        submitButton.interactable = true;
        inputField.ActivateInputField();

        Debug.Log("[Grader] Reset complete. Ready for next input.");
    }
}
