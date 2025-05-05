using UnityEngine;
using UnityEngine.EventSystems;

public class MovementBlocker : MonoBehaviour
{
    [SerializeField] private AnswerSheetToggle answerSheetToggle;

    private bool allowMovement = true;

    public bool IsMovementAllowed()
    {
        if (answerSheetToggle != null && answerSheetToggle.IsVisible())
        {
            // Check if you're currently typing in a field
            var selected = EventSystem.current?.currentSelectedGameObject;
            if (selected != null && selected.GetComponent<TMPro.TMP_InputField>() != null)
                return false;
        }

        return true;
    }
}
