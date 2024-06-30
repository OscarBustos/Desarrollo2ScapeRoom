using UnityEngine;

public class InputInventorySystem : PlayerSystem
{
    [Header("References")]
    [SerializeField] private InputManagerSO inputManager;

    #region Events

    private void OnEnable()
    {
        inputManager.OnScrollInventory += ScrollInventory;
    }

    private void ScrollInventory(float scroll)
    {
        if (scroll < 0)
        {

        }
        else if (scroll > 0)
        {

        }
    }

    private void OnDisable()
    {
        inputManager.OnScrollInventory -= ScrollInventory;
    }

    #endregion
}
