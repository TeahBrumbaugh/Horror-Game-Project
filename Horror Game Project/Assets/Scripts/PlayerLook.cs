
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{

    [Header("References")]
    public Transform playerBody;
    private Camera _camera;

    [Header("Input Actions")]
    public string lookActionName = "Look";
    public string lockToggleActionName = "LockToggle";
    public float mouseSensitivity = 60f;
    public float minimumPitch = -75f;
    public float maximumPitch = 75f;

    private float pitch = 0f;
    private InputAction lookAction;
    private InputAction lockToggleAction;
    private bool cursorLocked = true;
    void Awake()
    {
        _camera = GetComponent<Camera>();

        var playerInput = GetComponent<PlayerInput>();
        lookAction = playerInput.actions[lookActionName];
        lockToggleAction = playerInput.actions[lockToggleActionName];
    }

    void OnEnable()
    {
        lookAction.Enable();
        lockToggleAction.Enable();
        lockToggleAction.performed += OnLockToggle;

        LockCursor();
    }

    void OnDisable()
    {
        lockToggleAction.performed -= OnLockToggle;
        lookAction.Disable();
        lockToggleAction.Disable();
    }

    void Update()
    {

        if (!cursorLocked && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {

            } 
            else
            {
                LockCursor();
            }
        }

        if (cursorLocked)
        {
            Vector2 delta = lookAction.ReadValue<Vector2>();
            float mouseX = delta.x * mouseSensitivity * Time.deltaTime;
            float mouseY = delta.y * mouseSensitivity * Time.deltaTime;

            playerBody.Rotate(Vector3.up, mouseX);
            

            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, minimumPitch, maximumPitch);
            transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);


        }
    }

    private void OnLockToggle(InputAction.CallbackContext ctx)
    {

        if (cursorLocked) UnlockCursor();
        else LockCursor();
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorLocked = true;



    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cursorLocked = false;
    }
}
