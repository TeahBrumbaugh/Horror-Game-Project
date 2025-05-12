using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

[RequireComponent(typeof(PlayerInput))]
public class LockCursor : MonoBehaviour
{
    public string lockToggleActionName = "LockToggle";

    public CinemachineInputAxisController inputAxisController;
    private InputAction _lockToggleAction;
    private bool _cursorLocked = true;

    void Awake()
    {
        var pi = GetComponent<PlayerInput>();
        _lockToggleAction = pi.actions[lockToggleActionName];
    }

    void OnEnable()
    {
        _lockToggleAction.Enable();
        _lockToggleAction.performed += OnLockToggle;
        SetCursorState(true);
    }

    void OnDisable()
    {
        _lockToggleAction.performed -= OnLockToggle;
        _lockToggleAction.Disable();
    }

    private void OnLockToggle(InputAction.CallbackContext ctx)
    {
        if (PuzzleManager.Instance != null && PuzzleManager.Instance.IsPuzzleOpen())
        {
            PuzzleManager.Instance.CloseCurrentPuzzle();
            return;
        }
    
        SetCursorState(!_cursorLocked);
    }

    public void SetCursorState(bool locked)
    {
        _cursorLocked = locked;
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
        if (inputAxisController != null)
            inputAxisController.enabled = locked;
    }
}
