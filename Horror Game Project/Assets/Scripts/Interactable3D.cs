using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider))]
public class Interactable3D : MonoBehaviour
{
    public GameObject uiPanel;
    public TMP_InputField inputField;
    private AnswerSheetToggle answerSheetToggle;


    [Tooltip("Components to disable when UI is open")]
    public MonoBehaviour[] disableOnOpen;
    public LockCursor cameraLockController;


    private void Start()
    {
        answerSheetToggle = FindObjectOfType<AnswerSheetToggle>();
    }

    public void Interact()
    {
        foreach (var mb in disableOnOpen)
            mb.enabled = false;

        uiPanel.SetActive(true);
        inputField.text = "";
        inputField.ActivateInputField();

        if (cameraLockController != null)
            cameraLockController.SetCursorState(false);
        if (answerSheetToggle != null)
            answerSheetToggle.SetInPuzzle(true);
    }

    public void OnSubmit()
    {
        string entered = inputField.text;
        Debug.Log($"User entered: {entered}");

        // Hide UI
        uiPanel.SetActive(false);
        foreach (var mb in disableOnOpen)
            mb.enabled = true;

        // lock the cursor
        if (cameraLockController != null)
            cameraLockController.SetCursorState(true);
        if (answerSheetToggle != null)
            answerSheetToggle.SetInPuzzle(false);
    }

    public void CloseUI()
    {
        uiPanel.SetActive(false);
        foreach (var mb in disableOnOpen)
            mb.enabled = true;
        if (cameraLockController != null)
            cameraLockController.SetCursorState(true);
        if (answerSheetToggle != null)
            answerSheetToggle.SetInPuzzle(false);
    }
}
