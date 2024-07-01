using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class InputMovementSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputManagerSO inputManager;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 4.0f;
    [SerializeField] private float speedChangeRate = 10.0f;

    [Header("Gravity Settings")]
    [SerializeField] private float gravityFactor = -15.0f;
    [SerializeField] private float fallTimeout = 0.15f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float jumpTimeout = 0.1f;

    [Header("Ground Detection Settings")]
    [SerializeField] private float groundedOffset = -0.14f;
    [SerializeField] private float groundedRadius = 0.5f;
    [SerializeField] private LayerMask groundLayers;

    private CharacterController controller;

    private Vector3 inputDirection;
    private Vector3 moveDirection;

    private float horizontalSpeed;
    private float verticalSpeed;
    private float TERMINAL_VELOCITY = 53.0f;

    // timeout deltatime
    private float jumpTimeoutDelta;
    private float fallTimeoutDelta;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        jumpTimeoutDelta = jumpTimeout;
        fallTimeoutDelta = fallTimeout;

    }

    #region Events

    private void OnEnable()
    {
        inputManager.OnMove += Move;
        inputManager.OnJump += Jump;
    }

    private void Move(Vector2 ctx)
    {
        inputDirection = new Vector3(ctx.x, 0, ctx.y).normalized;
    }

    private void Jump()
    {
        if (AmIOnTheGround() && jumpTimeoutDelta <= 0.0f)
        {
            verticalSpeed = Mathf.Sqrt(-2 * gravityFactor * jumpHeight);
        }
    }

    private void OnDisable()
    {
        inputManager.OnMove -= Move;
        inputManager.OnJump -= Jump;
    }

    #endregion

    private void Update()
    {
        ApplyGravity();
        ApplyMovement();
    }

    #region Methods

    private void ApplyGravity()
    {
        if (AmIOnTheGround())
        {
            fallTimeoutDelta = fallTimeout;

            if (verticalSpeed < 0.0f)
                verticalSpeed = -2f;

            if (jumpTimeoutDelta >= 0.0f)
                jumpTimeoutDelta -= Time.deltaTime;
        }
        else
        {
            jumpTimeoutDelta = jumpTimeout;

            if (fallTimeoutDelta >= 0.0f)
                fallTimeoutDelta -= Time.deltaTime;
        }

        if (verticalSpeed < TERMINAL_VELOCITY)
            verticalSpeed += gravityFactor * Time.deltaTime;
    }


    private void ApplyMovement()
    {
        float targetSpeed = inputDirection.sqrMagnitude > 0 ? moveSpeed : 0.0f;
        float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;
        float speedOffset = 0.1f;

        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
            horizontalSpeed = Mathf.Round(Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * speedChangeRate) * 1000f) / 1000f;

        else
            horizontalSpeed = targetSpeed;

        if (inputDirection.sqrMagnitude > 0)
            moveDirection = transform.right * inputDirection.x + transform.forward * inputDirection.z;

        controller.Move(moveDirection.normalized * (horizontalSpeed * Time.deltaTime) + new Vector3(0.0f, verticalSpeed, 0.0f) * Time.deltaTime);
    }

    private bool AmIOnTheGround()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        return Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
    }

    #endregion

    #region Debug

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (AmIOnTheGround()) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z), groundedRadius);
    }

    #endregion
}
