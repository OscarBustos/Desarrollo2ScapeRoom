using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private GameManagerSO gameManager;
    [SerializeField] private ItemSO itemData;

    #region Events

    private void ItemAdded(bool added)
    {
        gameObject.SetActive(!added);
        gameManager.OnNewItemAdded -= ItemAdded;
    }

    #endregion

    #region IInteractable

    public bool CanInteract()
    {
        return gameObject.activeSelf;
    }

    public string GetInteractionText()
    {
        return itemData.ItemText;
    }

    public void Interact()
    {
        gameManager.AddNewItem(itemData);
        gameManager.OnNewItemAdded += ItemAdded;
    }

    #endregion
}
