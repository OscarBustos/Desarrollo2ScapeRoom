using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    private bool isOpen = false;

    public override bool CanInteract()
    {
        return true;
    }

    public override string GetInteractionText()
    {
        return isOpen ? "Close" : "Open";
    }

    public override void Interact()
    {
        isOpen = !isOpen;
        Debug.Log($"Door {(isOpen ? "opened" : "closed")}");

        base.Interact();
    }
}
