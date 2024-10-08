using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputBank : MonoBehaviour
{
    [field:SerializeField]public Vector2 moveVector {get; private set;}
    public bool isInteractPressed => _inputActions.Gameplay.Interact.IsPressed();
    public bool wasInteractPressedThisFrame => _inputActions.Gameplay.Interact.WasPressedThisFrame();
    
    private PlayerInputActions _inputActions;

    private void OnEnable()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Enable();

        _inputActions.Gameplay.Move.performed += OnMove;
        _inputActions.Gameplay.Move.canceled += OnMove;
        _inputActions.Gameplay.Interact.started += OnInteract;
    }

    void OnDisable()
    {
        _inputActions.Gameplay.Move.performed -= OnMove;
        _inputActions.Gameplay.Move.canceled -= OnMove;
        _inputActions.Gameplay.Interact.started -= OnInteract;
        
        _inputActions.Disable();
    }

    void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    void OnInteract(InputAction.CallbackContext context)
    {
        
    }
}
