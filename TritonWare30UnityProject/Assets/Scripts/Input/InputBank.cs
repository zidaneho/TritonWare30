using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputBank : MonoBehaviour
{
    public event Action RunCanceled;
    [field:SerializeField]public Vector2 moveVector {get; private set;}
    public bool isInteractPressed => _inputActions.Gameplay.Interact.IsPressed();
    public bool wasInteractPressedThisFrame => _inputActions.Gameplay.Interact.WasPressedThisFrame();
    public bool isRunPressed => _inputActions.Gameplay.Run.IsPressed();
    
    public bool wasFlashlightPressedThisFrame => _inputActions.Gameplay.Flashlight.WasPressedThisFrame();
    
    private PlayerInputActions _inputActions;

    private void OnEnable()
    {
        _inputActions = new PlayerInputActions();
        _inputActions.Enable();

        _inputActions.Gameplay.Move.performed += OnMove;
        _inputActions.Gameplay.Move.canceled += OnMove;
        _inputActions.Gameplay.Interact.started += OnInteract;
        _inputActions.Gameplay.Run.canceled += OnRun;
    }

    void OnDisable()
    {
        _inputActions.Gameplay.Move.performed -= OnMove;
        _inputActions.Gameplay.Move.canceled -= OnMove;
        _inputActions.Gameplay.Interact.started -= OnInteract;
        _inputActions.Gameplay.Run.canceled += OnRun;
        
        _inputActions.Disable();
    }

    void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    void OnInteract(InputAction.CallbackContext context)
    {
        
    }
    

    void OnRun(InputAction.CallbackContext context)
    {
        if (context.canceled) RunCanceled?.Invoke();
    }

    public void ToggleMoveInput(bool value)
    {
        if (value)
        {
            _inputActions.Gameplay.Move.performed += OnMove;
            _inputActions.Gameplay.Move.canceled += OnMove;
        }
        else
        {
            _inputActions.Gameplay.Move.performed -= OnMove;
            _inputActions.Gameplay.Move.canceled -= OnMove;

            moveVector = Vector2.zero;
        }
    }
}
