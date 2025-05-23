using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DelayedAnswerChecker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_InputField inputField;        // Player input
    [SerializeField] private MonoBehaviour puzzleScript;       // Must implement IAnswerProvider
    [SerializeField] private Button submitButton;
    [SerializeField] private StageManager stageManager;

    [Header("Settings")]
    [SerializeField] private float delay = 2f;                 // Delay in seconds before checking

    private IAnswerProvider answerProvider;

    private void Awake()
    {
        answerProvider = puzzleScript as IAnswerProvider;

        if (answerProvider == null)
        {
            Debug.LogError(" puzzleScript does not implement IAnswerProvider.");
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
        string submitted = inputField.text.Trim();
        string correct = answerProvider.GetCorrectAnswer().Trim();

        Debug.Log($" Grading '{submitted}' after {delay} seconds...");

        yield return new WaitForSeconds(delay);

        if (submitted.Equals(correct, System.StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log(" Correct answer!");
            stageManager?.GoToNextStage();
        }
        else
        {
            Debug.Log($" Incorrect. Expected: '{correct}'");

            // If it's a Wordle puzzle, update tile colors
            if (puzzleScript is WordleManager wordle)
            {
                wordle.UpdateCurrentRow(submitted);
            }

            stageManager?.FailAttempt();
        }

        inputField.text = "";  // Optional: reset input
    }
}
