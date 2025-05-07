using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private InputSystem_Actions playerInputActions;


    private void Awake()
    {
        playerInputActions = new InputSystem_Actions();
        playerInputActions.Player.Enable();

    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        
        return inputVector;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
    }

    private void OnDestroy()
    {
        playerInputActions.Dispose();
    }

}
