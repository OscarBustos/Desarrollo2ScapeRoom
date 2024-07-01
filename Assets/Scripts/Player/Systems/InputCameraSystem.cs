using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class InputCameraSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputManagerSO inputManager;

    [Header("Camera Settings")]
    [SerializeField] private float rotationSpeed = 1.0f;

    [Header("Cinemachine Settings")]
    [SerializeField] private GameObject cinemachineCameraTarget;
    [SerializeField] private float topClamp = 90.0f;
    [SerializeField] private float bottomClamp = -90.0f;

    private Vector2 inputRotation;

    // cinamachine
    private float cinemachineTargetPitch;

    // player
    private float rotationVelocity;

    private void Awake()
    {
        inputManager.SetCursorState(true);
    }

    #region Events

    private void OnEnable()
    {
        inputManager.OnLook += Look;
    }
    
    private void Look(Vector2 ctx)
    {
        inputRotation = ctx;
    }

    private void OnDisable()
    {
        inputManager.OnLook -= Look;
    }

    #endregion

    private void LateUpdate()
    {
        CameraRotation();
    }

    #region Methods

    private void CameraRotation()
    {

        cinemachineTargetPitch += inputRotation.y * rotationSpeed;
        rotationVelocity = inputRotation.x * rotationSpeed;

        cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, bottomClamp, topClamp);

        cinemachineCameraTarget.transform.localRotation = Quaternion.Euler(cinemachineTargetPitch, 0.0f, 0.0f);

        transform.Rotate(Vector3.up * rotationVelocity);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    #endregion
}
