using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/GameManager")]
public class GameManagerSO : ScriptableObject
{
    // UI Inventory
    public event Action<float> OnPlayerScrollInventory;
    public event Action<ItemSO> OnNewItem;
    public event Action<bool> OnNewItemAdded;
    public event Action<int> OnUpdateItemSelected;

    #region Public Methods

    public void PlayerScrollInventory(float scroll)
    {
        OnPlayerScrollInventory?.Invoke(scroll);
    }

    public void AddNewItem(ItemSO itemData)
    {
        OnNewItem?.Invoke(itemData);
    }

    public void NewItemAdded(bool added)
    {
        OnNewItemAdded?.Invoke(added);
    }

    public void UpdateItemSelected(int scrollDirection)
    {
        OnUpdateItemSelected?.Invoke(scrollDirection);
    }

    #endregion
}
