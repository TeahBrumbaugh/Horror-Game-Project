using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider))]
public class HintInteractables : MonoBehaviour
{
    public GameObject uiPanel;
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

        if (cameraLockController != null)
            cameraLockController.SetCursorState(false);
        if (answerSheetToggle != null)
            answerSheetToggle.SetInPuzzle(true);
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
