using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : InteractableObject
{
    public override bool CanInteract()
    {
        return true;
    }

    public override string GetInteractionText()
    {
        return "Collect";
    }
}
