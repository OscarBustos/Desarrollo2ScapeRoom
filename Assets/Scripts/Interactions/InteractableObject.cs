
using System;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    public abstract bool CanInteract();
    public virtual void Interact()
    {
        // Call this at the end
        NotifyInteraction();
    }

    public abstract string GetInteractionText();

    protected void NotifyInteraction()
    {
        EventManager.ShowInteraction(GetInteractionText());
    }
}
