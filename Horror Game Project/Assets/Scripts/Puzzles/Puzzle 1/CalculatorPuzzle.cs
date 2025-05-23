using UnityEngine;
using TMPro;

public class CalculatorPuzzle : MonoBehaviour, IAnswerProvider, IPuzzleResettable
{
    public enum Difficulty { Easy, Medium, Hard }
    public enum Operation { Add, Subtract, Multiply, Divide }

    [Header("UI References")]
    [SerializeField] private TMP_InputField answerInput;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text attemptText;

    [Header("Puzzle Settings")]
    [SerializeField] private Difficulty selectedDifficulty = Difficulty.Easy;
    [SerializeField] private int maxAttempts = 3;
    public JumpScare jumpscare;

    private int A, B, modulus, correctAnswer, currentAttempt = 0;
    private Operation currentOperation;
    private bool puzzleActive = true;

    private void Start()
    {
        GenerateNewProblem();
    }

    private void GenerateNewProblem()
    {
        switch (selectedDifficulty)
        {
            case Difficulty.Easy:
                A = Random.Range(0, 10);
                B = Random.Range(0, 10);
                modulus = Random.Range(1, 5);
                currentOperation = Random.value < 0.5f ? Operation.Add : Operation.Subtract;
                break;
            case Difficulty.Medium:
                A = Random.Range(10, 50);
                B = Random.Range(10, 50);
                modulus = Random.Range(5, 10);
                currentOperation = (Operation)Random.Range(0, 4);
                break;
            case Difficulty.Hard:
                A = Random.Range(50, 200);
                B = Random.Range(50, 200);
                modulus = Random.Range(3, 19);
                currentOperation = (Operation)Random.Range(0, 4);
                break;
        }

        FixForPositiveAnswers();
        correctAnswer = CalculateOperation(A, B) % modulus;

        questionText.text = $"What is ({A} {GetOperatorSymbol(currentOperation)} {B}) mod {modulus}?";
        resultText.text = "";
        currentAttempt = 0;
        attemptText.text = $"Attempt: {currentAttempt + 1}/{maxAttempts}";
        puzzleActive = true;
        answerInput.interactable = true;
        answerInput.text = "";
        answerInput.ActivateInputField();
    }

    private int CalculateOperation(int a, int b)
    {
        return currentOperation switch
        {
            Operation.Add => a + b,
            Operation.Subtract => a - b,
            Operation.Multiply => a * b,
            Operation.Divide => b != 0 ? a / b : 0,
            _ => 0,
        };
    }

    private void FixForPositiveAnswers()
    {
        if (currentOperation == Operation.Subtract && B > A)
        {
            (A, B) = (B, A);
        }
        else if (currentOperation == Operation.Divide)
        {
            B = Random.Range(1, selectedDifficulty == Difficulty.Hard ? 20 : 10);
            A = B * Random.Range(1, selectedDifficulty == Difficulty.Hard ? 10 : 5);
        }
    }

    private string GetOperatorSymbol(Operation op) => op switch
    {
        Operation.Add => "+",
        Operation.Subtract => "-",
        Operation.Multiply => "x",
        Operation.Divide => "÷",
        _ => "?"
    };

    public void SubmitAnswer()
    {
        if (!puzzleActive) return;

        if (int.TryParse(answerInput.text.Trim(), out int playerAnswer))
        {
            if (playerAnswer == correctAnswer)
            {
                resultText.text = "Correct!";
                puzzleActive = false;
                answerInput.interactable = false;
            }
            else
            {
                currentAttempt++;
                if (currentAttempt >= maxAttempts)
                {
                    jumpscare?.TriggerJumpScare();
                    resultText.text = $"Game Over! Correct Answer: {correctAnswer}";
                    puzzleActive = false;
                    answerInput.interactable = false;
                }
                else
                {
                    resultText.text = "Incorrect. Try again!";
                    attemptText.text = $"Attempt: {currentAttempt + 1}/{maxAttempts}";
                }
            }
        }
        else
        {
            resultText.text = "Please enter a valid number.";
        }

        answerInput.text = "";
        answerInput.ActivateInputField();
    }

    public void ResetPuzzle() => GenerateNewProblem();
    public string GetCorrectAnswer() => correctAnswer.ToString();
    public int GetMaxAttempts() => maxAttempts;
}
