using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WordleManager : MonoBehaviour, IAnswerProvider, IPuzzleResettable
{
    [Header("UI References")]
    [SerializeField] private TMP_InputField playerInput;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_Text attemptText;
    [SerializeField] private Transform boardParent;
    [SerializeField] private GameObject letterTilePrefab;

    [Header("Puzzle Settings")]
    [SerializeField] private TextAsset wordListText;
    [SerializeField] private int maxAttempts = 6;
    [SerializeField] private int wordLength = 5;
    public JumpScare jumpscare;

    private List<string> words = new List<string>();
    private List<GameObject> currentTiles = new List<GameObject>();

    private string targetWord;
    private int currentAttempt = 0;
    private bool puzzleActive = true;

    private void Start()
    {
        LoadWords();
        GenerateNewPuzzle();
    }

    private void LoadWords()
    {
        words.Clear();
        string[] lines = wordListText.text.Split('\n');

        foreach (string line in lines)
        {
            string word = line.Trim().ToLower();
            if (word.Length == wordLength)
            {
                words.Add(word);
            }
        }

        if (words.Count == 0)
            Debug.LogError("No valid words found in word list!");
    }

    private void GenerateNewPuzzle()
    {
        ClearBoard();
        currentAttempt = 0;
        puzzleActive = true;
        playerInput.interactable = true;
        UpdateAttemptText();

        targetWord = words[Random.Range(0, words.Count)];
        Debug.Log($"[WordlePuzzle] Target word: {targetWord}");

        messageText.text = "Make a Guess";
        CreateEmptyRow();

        playerInput.text = "";
        playerInput.ActivateInputField();
    }

    private void UpdateAttemptText()
    {
        attemptText.text = $"Attempt {currentAttempt + 1} / {maxAttempts}";
    }

    public void SubmitGuess()
    {
        if (!puzzleActive) return;

        string guess = playerInput.text.Trim().ToUpper();

        if (guess.Length != wordLength)
        {
            messageText.text = $"Guess must be {wordLength} letters!";
            return;
        }

        if (!words.Contains(guess))
        {
            messageText.text = "Not a valid word!";
            return;
        }

        UpdateCurrentRow(guess);
        currentAttempt++;
        UpdateAttemptText();

        if (guess == targetWord)
        {
            messageText.text = "You Win!";
            puzzleActive = false;
            playerInput.interactable = false;
        }
        else if (currentAttempt >= maxAttempts)
        {
            jumpscare?.TriggerJumpScare();
            messageText.text = $"Game Over! Word was {targetWord}";
            puzzleActive = false;
            playerInput.interactable = false;
        }

        playerInput.text = "";
        playerInput.ActivateInputField();
    }

    private void CreateEmptyRow()
    {
        currentTiles.Clear();

        for (int i = 0; i < wordLength; i++)
        {
            GameObject tile = Instantiate(letterTilePrefab, boardParent);
            TMP_Text tileText = tile.GetComponentInChildren<TMP_Text>();
            tileText.text = "";
            tile.GetComponent<Image>().color = Color.gray;

            currentTiles.Add(tile);
        }
    }

    public void UpdateCurrentRow(string guess)
    {
        if (guess.Length < wordLength)
        {
            Debug.LogWarning($"[WordleManager] Guess '{guess}' is too short. Skipping color update.");
            return;
        }

        for (int i = 0; i < wordLength; i++)
        {
            GameObject tile = currentTiles[i];
            TMP_Text tileText = tile.GetComponentInChildren<TMP_Text>();
            tileText.text = guess[i].ToString();

            var img = tile.GetComponent<Image>();
            if (guess[i] == targetWord[i])
                img.color = Color.green;
            else if (targetWord.Contains(guess[i].ToString()))
                img.color = Color.yellow;
            else
                img.color = Color.gray;
        }
    }

    private void ClearBoard()
    {
        foreach (Transform child in boardParent)
        {
            Destroy(child.gameObject);
        }
        currentTiles.Clear();
    }

    public void ResetPuzzle() => GenerateNewPuzzle();
    public string GetCorrectAnswer() => targetWord;
    public int GetMaxAttempts() => maxAttempts;
}
