using UnityEngine;
using TMPro;

public class AnswerSheetInputPageManager : MonoBehaviour
{
    public TMP_InputField puzzle1Input;
    public TMP_InputField puzzle2Input;
    public TMP_Text feedbackText;

    [Header("Correct Answers")]
    [SerializeField] private string correctPuzzle1Answer = "12";
    [SerializeField] private string correctPuzzle2Answer = "CRANE";

    public bool IsCorrect1 { get; private set; }
    public bool IsCorrect2 { get; private set; }
    public string Answer1 => puzzle1Input.text.Trim().ToUpper();
    public string Answer2 => puzzle2Input.text.Trim().ToUpper();

    public void SubmitAnswers()
    {
        IsCorrect1 = (Answer1 == correctPuzzle1Answer.ToUpper());
        IsCorrect2 = (Answer2 == correctPuzzle2Answer.ToUpper());

        feedbackText.text = $"Puzzle 1: {(IsCorrect1 ? "O" : "X")} | " +
                            $"Puzzle 2: {(IsCorrect2 ? "O" : "X")}";
    }
}
