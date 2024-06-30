using UnityEngine;

public class InputInventorySystem : PlayerSystem
{
    [Header("References")]
    [SerializeField] private InputManagerSO inputManager;
    [SerializeField] private GameManagerSO gameManager;

    #region Events

    private void OnEnable()
    {
        inputManager.OnScrollInventory += ScrollInventory;
    }

    private void ScrollInventory(float scroll)
    {
        gameManager.UpdateItemSelected(scroll > 0 ? 1 : (scroll < 0 ? -1 : 0));
    }

    private void OnDisable()
    {
        inputManager.OnScrollInventory -= ScrollInventory;
    }

    #endregion
}
