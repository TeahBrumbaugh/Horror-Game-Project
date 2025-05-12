using TMPro;
using UnityEngine;

public class AnswerSheetUI : MonoBehaviour
{
    [SerializeField] private TMP_Text answerText;
    private string currentAnswer;

    public void SetAnswer(string answer)
    {
        currentAnswer = answer;
        answerText.text = answer;
    }

    public string GetAnswer()
    {
        return currentAnswer;
    }
}
