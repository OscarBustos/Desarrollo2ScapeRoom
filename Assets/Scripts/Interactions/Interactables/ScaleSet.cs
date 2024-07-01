using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScaleSet : InteractableObject
{
    [SerializeField] TextMeshProUGUI screenText;
    private ScaleAction scaleAction;

    private GameObject item;
    private bool itemAdded;


    private void Awake()
    {
        scaleAction = GetComponentInChildren<ScaleAction>();
    }

    #region Interface implementation
    public override bool CanInteract()
    {
        return item ? true : false;
    }

    public override string GetInteractionText()
    {
        return itemAdded ? "Remove Item" : "Weight Item";
    }

    public override void Interact()
    {
        if (itemAdded)
        {
            scaleAction.RemoveItem(item);
            itemAdded = false;
        } 
        else
        {
            scaleAction.AddItem(item);
            itemAdded = true;
            
        }
        scaleAction.Execute();
        screenText.text = scaleAction.Result;
        base.Interact();
    }

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IInteractable>() != null)
        {
            item = other.gameObject;
        }
    }

}
