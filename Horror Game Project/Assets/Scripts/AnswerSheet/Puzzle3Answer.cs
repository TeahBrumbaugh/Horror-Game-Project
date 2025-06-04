using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Puzzle3Answer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button submitButton;
    [SerializeField] private StageManager stageManager;
    [SerializeField] private GameObject blackoutPanel;

    [Header("Settings")]
    [SerializeField] private float delay = 2f;

    private void Awake()
    {
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(() => StartCoroutine(GradeAfterDelay()));
        }
    }

    private IEnumerator GradeAfterDelay()
    {
        //  Turn on blackout
        if (blackoutPanel != null)
            blackoutPanel.SetActive(true);

        string submitted = inputField.text.Trim();
        string correct = "9942";

        Debug.Log($" Grading '{submitted}' after {delay} seconds...");

        yield return new WaitForSeconds(delay);

        //  Turn off blackout
        if (blackoutPanel != null)
            blackoutPanel.SetActive(false);

        if (submitted.Equals(correct, System.StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log(" Correct answer!");
            SceneManager.LoadScene("WIN");
        }
        else
        {
            Debug.Log($" Incorrect. Expected: '{correct}'");
            stageManager?.FailAttempt();
        }

        inputField.text = "";
    }
}
