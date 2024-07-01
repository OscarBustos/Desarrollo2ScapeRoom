using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField, Tooltip("Distance at which the player can interact with other objects in meters")]
    private float interactDistance = 3f;
    [SerializeField] private LayerMask interactableLayers;

    private PlayerInputs inputs;
    private IInteractable currentInteractable;

    private void Awake()
    {
        inputs = GetComponent<PlayerInputs>();
    }

    private void Update()
    {
        CheckForInteractable();

        if (currentInteractable != null && inputs.interact) 
        {
            currentInteractable.Interact();
            inputs.interact = false;
        }
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
