using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Puzzle3Answer : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button submitButton;
    [SerializeField] private StageManager stageManager;
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
        string submitted = inputField.text.Trim();
        string correct = "1618";

        Debug.Log($" Grading '{submitted}' after {delay} seconds...");

        yield return new WaitForSeconds(delay);

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
