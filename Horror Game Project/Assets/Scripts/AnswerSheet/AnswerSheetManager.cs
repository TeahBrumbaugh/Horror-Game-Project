using UnityEngine;
using TMPro;

public class AnswerSheetManager : MonoBehaviour
{
    [Header("Answer Fields")]
    [SerializeField] private TMP_InputField puzzle1AnswerField;
    [SerializeField] private TMP_InputField puzzle2AnswerField;

    [Header("Correct Answers (for validation)")]
    [SerializeField] private string correctPuzzle1Answer = "12"; // example: calculator answer
    [SerializeField] private string correctPuzzle2Answer = "CRANE"; // example: Wordle answer

    [Header("UI Feedback")]
    [SerializeField] private TMP_Text feedbackText;

    public string Puzzle1Answer { get; private set; }
    public string Puzzle2Answer { get; private set; }
    public bool Puzzle1IsCorrect { get; private set; }
    public bool Puzzle2IsCorrect { get; private set; }

    public void SubmitAnswers()
    {
        Puzzle1Answer = puzzle1AnswerField.text.Trim().ToUpper();
        Puzzle2Answer = puzzle2AnswerField.text.Trim().ToUpper();

        Puzzle1IsCorrect = Puzzle1Answer == correctPuzzle1Answer.ToUpper();
        Puzzle2IsCorrect = Puzzle2Answer == correctPuzzle2Answer.ToUpper();

        // Store responses no matter what
        Debug.Log($"Puzzle 1 Answer: {Puzzle1Answer} | Correct: {Puzzle1IsCorrect}");
        Debug.Log($"Puzzle 2 Answer: {Puzzle2Answer} | Correct: {Puzzle2IsCorrect}");

        feedbackText.text = "Answers Saved!\n" +
            $"Puzzle 1: {(Puzzle1IsCorrect ? "✅" : "❌")} | " +
            $"Puzzle 2: {(Puzzle2IsCorrect ? "✅" : "❌")}";
    }
}
