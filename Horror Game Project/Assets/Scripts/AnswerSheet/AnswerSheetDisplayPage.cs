using UnityEngine;
using TMPro;

public class AnswerSheetDisplayPage : MonoBehaviour
{
    public TMP_Text puzzle1Text;
    public TMP_Text puzzle2Text;
    public TMP_Text feedbackText;

    public void SetAnswers(string answer1, string answer2, bool isCorrect1, bool isCorrect2)
    {
        puzzle1Text.text = $"Puzzle 1 Answer: {answer1}";
        puzzle2Text.text = $"Puzzle 2 Answer: {answer2}";
        feedbackText.text = $"Result: {(isCorrect1 ? "O" : "X")} | {(isCorrect2 ? "O" : "X")}";
    }
}
