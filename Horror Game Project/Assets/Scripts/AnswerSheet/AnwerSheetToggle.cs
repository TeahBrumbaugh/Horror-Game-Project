using UnityEngine;
using UnityEngine.EventSystems;

public class AnswerSheetToggle : MonoBehaviour
{
    [SerializeField] private GameObject answerSheetPanel;
    [SerializeField] private KeyCode toggleKey = KeyCode.E;    // Customize as needed
    [SerializeField] private bool inPuzzle = true;

    private bool isVisible = false;

    void Start()
    {
        isVisible = false;
        answerSheetPanel.SetActive(false);
        Debug.Log("[AnswerSheetToggle] Initialized. Panel hidden.");
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("[AnswerSheetToggle] A key was pressed.");
        }
        EventSystem.current.SetSelectedGameObject(null);

        if (Input.GetKeyDown(toggleKey) && !inPuzzle)
        {
            ToggleAnswerSheet();
        }
    }

    private void ToggleAnswerSheet()
    {
        isVisible = !isVisible;
        answerSheetPanel.SetActive(isVisible);
        Debug.Log($"[AnswerSheetToggle] Answer sheet visibility set to: {isVisible}");
    }

    // Call this from puzzle scripts when entering/exiting puzzles
    public void SetInPuzzle(bool value)
    {
        inPuzzle = value;
        Debug.Log($"[AnswerSheetToggle] inPuzzle set to: {inPuzzle}");

        // Optional: force hide if puzzle starts
        if (inPuzzle && isVisible)
        {
            isVisible = false;
            answerSheetPanel.SetActive(false);
            Debug.Log("[AnswerSheetToggle] Puzzle started. Hiding answer sheet.");
        }
    }
}