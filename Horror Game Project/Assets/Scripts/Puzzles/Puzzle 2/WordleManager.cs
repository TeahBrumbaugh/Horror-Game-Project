using System.Collections.Generic;
using UnityEngine;
using TMPro; // if using TextMeshPro

public class WordleManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private TextAsset wordListText;
    [SerializeField] private GameObject letterTilePrefab;
    [SerializeField] private Transform boardParent;
    [SerializeField] private TMP_InputField playerInput;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private int maxAttempts = 6;
    [SerializeField] private TMP_Text attemptText;


    private List<string> words = new List<string>();
    private string targetWord;
    private int currentAttempt = 0;
    public JumpScare jumpscare;

    private void UpdateAttemptText()
    {
        attemptText.text = $"Attempt {currentAttempt + 1} / {maxAttempts}";
    }


    private void Start()
    {
        LoadWords();
        StartNewGame();
    
    }


    private void LoadWords()
    {
        words = new List<string>();

        string[] lines = wordListText.text.Split('\n');
        foreach (string line in lines)
        {
            string word = line.Trim().ToUpper();
            if (word.Length == 5)
            {
                words.Add(word);
                Debug.Log("Loaded word: " + word);
            }
            else
            {
                Debug.LogWarning("Skipped word: " + word);
            }
        }
    }


    private void StartNewGame()
    {
        currentAttempt = 0;
        UpdateAttemptText();
        targetWord = words[Random.Range(0, words.Count)];
        Debug.Log("Target Word: " + targetWord);
        currentAttempt = 0;
        messageText.text = "Make a Guess";
    }

    public void SubmitGuess()
    {
        string guess = playerInput.text.Trim().ToUpper();
        if (guess.Length != 5)
        {
            messageText.text = "Guess must be 5 letters!";
            return;
        }

        if (!words.Contains(guess))
        {
            messageText.text = "Not a valid word!";
        }

        CreateRow(guess);
        currentAttempt++;
        UpdateAttemptText();  // << THIS must be here!!

        if (guess == targetWord)
        {
            messageText.text = "You Win!";
            playerInput.interactable = false;
        }
        else if (currentAttempt >= maxAttempts)
        {
            jumpscare.TriggerJumpScare();
            messageText.text = $"Game Over! Word was {targetWord}";
            playerInput.interactable = false;
        }

        playerInput.text = "";
    }


    private void CreateRow(string guess)
    {
        Debug.Log($"Creating Row for Guess: {guess}");

        for (int i = 0; i < guess.Length; i++)
        {
            GameObject tile = Instantiate(letterTilePrefab, boardParent);
            TMP_Text tileText = tile.GetComponentInChildren<TMP_Text>();
            tileText.text = guess[i].ToString();

            if (guess[i] == targetWord[i])
            {
                tile.GetComponent<UnityEngine.UI.Image>().color = Color.green;
            }
            else if (targetWord.Contains(guess[i].ToString()))
            {
                tile.GetComponent<UnityEngine.UI.Image>().color = Color.yellow;
            }
            else
            {
                tile.GetComponent<UnityEngine.UI.Image>().color = Color.gray;
            }
        }
    }

}
