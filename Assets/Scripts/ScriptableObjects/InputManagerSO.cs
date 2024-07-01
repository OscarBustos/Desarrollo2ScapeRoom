using System;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

[CreateAssetMenu(menuName = "Managers/InputManager")]
public class InputManagerSO : ScriptableObject
{
    private Controls controls;

    public event Action<Vector2> OnMove, OnLook;
    public event Action OnJump, OnInteract;
    public event Action<float> OnScrollInventory;

    #region Events

    private void OnEnable()
    {
        if (controls == null)
        {
            controls = new Controls();
            EnablePlayerInputs();
        }
        
        controls.Enable();
        Debug.Log("Input Manager Ready!");

    }

    private void Move(InputAction.CallbackContext context)
    {
        OnMove?.Invoke(context.ReadValue<Vector2>());
    }

    private void Look(InputAction.CallbackContext context)
    {
        OnLook?.Invoke(context.ReadValue<Vector2>());
    }

    private void Jump(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }

    private void ScrollInventory(InputAction.CallbackContext context)
    {
        OnScrollInventory?.Invoke(context.ReadValue<float>());
    }

    private void Interact(InputAction.CallbackContext context)
    {
        OnInteract?.Invoke();
    }

    private void OnDisable()
    {
        if (controls != null)
        {
            DisablePlayerInputs();

            controls.Disable();
            Debug.Log("Input Disabled!");
        }
    }


    #endregion

    #region Enable And Disable Input Maps

    #region Player Movement Input Map

    private void EnablePlayerMovement()
    {
        controls.PlayerMovement.Move.performed += Move;
        controls.PlayerMovement.Move.canceled += Move;

        controls.PlayerMovement.Jump.performed += Jump;

        controls.PlayerMovement.Look.performed += Look;
        controls.PlayerMovement.Look.canceled += Look;
    }

    private void DisablePlayerMovement()
    {
        controls.PlayerMovement.Move.performed -= Move;
        controls.PlayerMovement.Move.canceled -= Move;

        controls.PlayerMovement.Jump.performed -= Jump;

        controls.PlayerMovement.Look.performed -= Look;
        controls.PlayerMovement.Look.canceled -= Look;
    }

    #endregion

    #region Player Actions Input Map

    private void EnablePlayerActions()
    {
        controls.PlayerActions.ScrollInventory.performed += ScrollInventory;

        controls.PlayerActions.Interact.performed += Interact;
    }

    private void DisablePlayerActions()
    {
        controls.PlayerActions.ScrollInventory.performed -= ScrollInventory;

        controls.PlayerActions.Interact.performed -= Interact;
    }

    #endregion

    private void EnablePlayerInputs()
    {
        EnablePlayerMovement();
        EnablePlayerActions();
    }

    private void DisablePlayerInputs()
    {
        DisablePlayerMovement();
        DisablePlayerActions();
    }

    #endregion

    #region Public Methods

    public void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

    #endregion
}
