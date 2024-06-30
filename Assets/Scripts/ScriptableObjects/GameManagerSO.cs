using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/GameManager")]
public class GameManagerSO : ScriptableObject
{
    // UI Inventory
    public event Action<float> OnPlayerScrollInventory;

    #region Public Methods

    public void PlayerScrollInventory(float scroll)
    {
        OnPlayerScrollInventory?.Invoke(scroll);
    }

    #endregion
}
