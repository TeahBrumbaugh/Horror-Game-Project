using UnityEngine;
using System.Collections;

public class AnswerChecker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AnswerSheetUI answerSheet;
    [SerializeField] private MonoBehaviour puzzleScript; // Must implement IAnswerProvider
    [SerializeField] private StageManager stageManager;
    [SerializeField] private float checkDelay = 2f;

    private IAnswerProvider answerProvider;
    private bool isChecking = false;

    private void Awake()
    {
        answerProvider = puzzleScript as IAnswerProvider;

        if (answerProvider == null)
            Debug.LogError($"{puzzleScript.name} does not implement IAnswerProvider.");
    }

    // This triggers when the player clicks directly on the desk's collider
    private void OnMouseDown()
    {
        if (!isChecking)
        {
            StartCoroutine(CheckAnswerRoutine());
        }
    }

    private IEnumerator CheckAnswerRoutine()
    {
        isChecking = true;

        Debug.Log("Grading in progress...");
        yield return new WaitForSeconds(checkDelay);

        string submitted = answerSheet.GetAnswer().Trim();
        string correct = answerProvider.GetCorrectAnswer().Trim();

        if (submitted.Equals(correct, System.StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("Correct!");
            stageManager.GoToNextStage();
        }
        else
        {
            Debug.Log("Incorrect.");
            if (puzzleScript is WordleManager wordle)
            {
                wordle.UpdateCurrentRow(submitted); 
            }

            stageManager.FailAttempt();
        }

        isChecking = false;
    }
}
