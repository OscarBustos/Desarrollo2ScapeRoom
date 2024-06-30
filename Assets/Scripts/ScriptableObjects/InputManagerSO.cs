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

    public event Action<Vector2> OnMove;
    public event Action<Vector2> OnLook;
    public event Action<bool> OnJump;
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

    private void Jump(InputAction.CallbackContext context)
    {
        OnJump?.Invoke(context.ReadValueAsButton());
    }

    private void ScrollInventory(InputAction.CallbackContext context)
    {
        OnScrollInventory?.Invoke(context.ReadValue<float>());
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
    }

    private void DisablePlayerMovement()
    {
        controls.PlayerMovement.Move.performed -= Move;
        controls.PlayerMovement.Move.canceled -= Move;

        controls.PlayerMovement.Jump.performed -= Jump;
    }

    #endregion

    #region Player Actions Input Map

    private void EnablePlayerActions()
    {
        controls.PlayerActions.ScrollInventory.performed += ScrollInventory;
    }

    private void DisablePlayerActions()
    {
        controls.PlayerActions.ScrollInventory.performed -= ScrollInventory;
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
}
