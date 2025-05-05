using UnityEngine;

public class AnswerSheetToggle : MonoBehaviour
{
    [SerializeField] private GameObject answerSheetPanel;
    [SerializeField] private KeyCode toggleKey = KeyCode.Tab;

    private bool isVisible = false;
    private bool canToggle = false;

    void Start()
    {
        SetPanelVisible(false);
        canToggle = true;
    }

    void Update()
    {
        if (!canToggle) return;

        if (Input.GetKeyDown(toggleKey))
        {
            isVisible = !isVisible;
            SetPanelVisible(isVisible);
        }
    }

    public void EnableToggle()
    {
        canToggle = true;
        isVisible = true;
        SetPanelVisible(true);
    }

    public void DisableToggle()
    {
        canToggle = false;
        isVisible = false;
        SetPanelVisible(false);
    }

    private void SetPanelVisible(bool show)
    {
        if (answerSheetPanel != null)
            answerSheetPanel.SetActive(show);
    }

    public bool IsVisible() => isVisible;
    public bool CanToggle() => canToggle;
}
