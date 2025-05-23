using UnityEngine;
using TMPro;

public class CalculatorPuzzle : MonoBehaviour, IAnswerProvider, IPuzzleResettable
{
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    public enum Operation
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    [Header("UI References")]
    [SerializeField] private TMP_InputField answerInput;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text attemptText;
    [SerializeField] private AnswerSheetUI answerSheet;

    [Header("Puzzle Settings")]
    [SerializeField] private Difficulty selectedDifficulty = Difficulty.Easy;
    [SerializeField] private int maxAttempts = 3;

    private int A;
    private int B;
    private int modulus;
    private int correctAnswer;
    private Operation currentOperation;
    private int currentAttempt = 0;
    private bool puzzleActive = true;
    public JumpScare jumpscare;
    private static bool initialized = false;

    private void Start()
    {
        GenerateNewProblem();
    }

    public void ResetPuzzle()
    {
        GenerateNewProblem(); // Reset numbers, UI, and answer
    }



    private void GenerateNewProblem()
    {
        // Set difficulty ranges
        switch (selectedDifficulty)
        {
            case Difficulty.Easy:
                A = Random.Range(0, 10);
                B = Random.Range(0, 10);
                modulus = Random.Range(1, 5); ;
                currentOperation = (Random.value < 0.5f) ? Operation.Add : Operation.Subtract;
                break;

            case Difficulty.Medium:
                A = Random.Range(10, 50);
                B = Random.Range(10, 50);
                modulus = Random.Range(5, 10);
                currentOperation = (Operation)Random.Range(0, 4); // 0=Add, 1=Subtract, 2=Multiply, 3=Divide
                break;

            case Difficulty.Hard:
                A = Random.Range(50, 200);
                B = Random.Range(50, 200);
                modulus = Random.Range(3, 19);
                currentOperation = (Operation)Random.Range(0, 4);
                break;
        }

        // Fix numbers for positive answers
        FixForPositiveAnswers();

        // Calculate correct answer
        int rawResult = CalculateOperation(A, B);
        correctAnswer = rawResult % modulus;

        // Update UI
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
        switch (currentOperation)
        {
            case Operation.Add: return a + b;
            case Operation.Subtract: return a - b;
            case Operation.Multiply: return a * b;
            case Operation.Divide: return a / b;
            default: return 0;
        }
    }

    private void FixForPositiveAnswers()
    {
        if (currentOperation == Operation.Subtract)
        {
            // Ensure a >= b
            if (B > A)
            {
                int temp = A;
                A = B;
                B = temp;
            }
        }
        else if (currentOperation == Operation.Divide)
        {
            // Ensure clean division
            // Pick B first to avoid division by zero
            B = Random.Range(1, (selectedDifficulty == Difficulty.Hard) ? 20 : 10);

            int tempMultiplier = Random.Range(1, (selectedDifficulty == Difficulty.Hard) ? 10 : 5);
            A = B * tempMultiplier;
        }
    }

    private string GetOperatorSymbol(Operation op)
    {
        switch (op)
        {
            case Operation.Add: return "+";
            case Operation.Subtract: return "-";
            case Operation.Multiply: return "x";
            case Operation.Divide: return "÷";
            default: return "?";
        }
    }

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
                answerInput.placeholder.GetComponent<TMP_Text>().text = "Puzzle Completed!";
            }
            else
            {
                currentAttempt++;
                if (currentAttempt >= maxAttempts)
                {
                    jumpscare.TriggerJumpScare();
                    resultText.text = $"Game Over! Correct Answer: {correctAnswer}";
                    puzzleActive = false;
                    answerInput.interactable = false;
                    answerInput.placeholder.GetComponent<TMP_Text>().text = "Puzzle Over!";
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

    public void OnCalculatorSubmit()
    {
        string result = answerInput.text.Trim();

        if (!string.IsNullOrEmpty(result))
        {
            answerSheet.SetAnswer(result);
            answerInput.text = "";  // Optional: clear after submit
        }
    }

    public string GetCorrectAnswer()
    {
        return correctAnswer.ToString(); // convert int to string
    }

}