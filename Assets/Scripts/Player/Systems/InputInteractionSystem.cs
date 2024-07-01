using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInteractionSystem : PlayerSystem
{
    [Header("References")]
    [SerializeField] private InputManagerSO inputManager;

    [Header("Interact Settings")]
    [SerializeField, Tooltip("Distance at which the player can interact with other objects in meters")]
    private float interactDistance = 3f;
    [SerializeField] private LayerMask interactableLayers;

    private IInteractable currentInteractable;

    #region Events

    private void OnEnable()
    {
        inputManager.OnInteract += Interact;
    }

    private void Interact()
    {
        if (currentInteractable != null)
        {
            currentInteractable?.Interact();
        }
    }

    private void OnDisable()
    {
        inputManager.OnInteract -= Interact;
    }

    #endregion

    private void Update()
    {
        CheckForInteractable();
    }

    private void CheckForInteractable()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactableLayers))
        {
            Debug.Log("Interact recognisable");
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                if (interactable.CanInteract())
                {
                    if (currentInteractable != interactable)
                    {
                        currentInteractable = interactable;
                        InteractableObject interactableObject = interactable as InteractableObject;
                        if (interactableObject != null)
                        {
                            EventManager.ShowInteraction(interactableObject.GetInteractionText());
                        }
                    }
                }
                return;
            }
        }

        currentInteractable = null;
        EventManager.HideInteraction();
    }
}
