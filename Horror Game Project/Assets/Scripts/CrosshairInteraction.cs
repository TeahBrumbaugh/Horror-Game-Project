using UnityEngine;
using UnityEngine.UI;           // for Image
using UnityEngine.InputSystem;  // new Input System

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Camera))]
public class CrosshairInteraction : MonoBehaviour
{
    public Image crosshairImage;
    public Color normalColor = Color.white;
    public Color highlightColor = Color.red;
    public float interactDistance = 2f;
    public LayerMask interactableLayer;
    public string clickActionName = "Interact";  

    private Camera _camera;
    private PlayerInput _playerInput;
    private InputAction _clickAction;

    void Awake()
    {
        _camera      = GetComponent<Camera>();
        _playerInput = GetComponent<PlayerInput>();
        _clickAction = _playerInput.actions[clickActionName];

        if (crosshairImage) 
            crosshairImage.color = normalColor;
    }

    void OnEnable()
    {
        _clickAction.Enable();
        _clickAction.performed += OnClickPerformed;
    }

    void OnDisable()
    {
        _clickAction.performed -= OnClickPerformed;
        _clickAction.Disable();
    }

    void Update()
    {
        var ray = _camera.ScreenPointToRay(
            new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f)
        );

        bool hitSomething = Physics.Raycast(ray, out RaycastHit hit, interactDistance,interactableLayer);

        if (crosshairImage)
        {
            crosshairImage.color = hitSomething ? highlightColor : normalColor;
        }    
    }

    private void OnClickPerformed(InputAction.CallbackContext ctx)
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            return;
        }

        var ray = _camera.ScreenPointToRay(
            new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f)
        );

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactableLayer))
        {
            var puzzle =hit.collider.GetComponent<PuzzleInteractable>();
            var interactable = hit.collider.GetComponent<Interactable3D>();
            if (interactable != null)
                interactable.Interact();
            if(puzzle != null)
            {
                puzzle.Interact();
                return;
            }
        }
    }
}
