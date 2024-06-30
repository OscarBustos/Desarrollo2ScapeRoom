using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManagerSO gameManager;

    [Header("Canvas Elements")]
    [SerializeField] private TextMeshProUGUI itemInUseName;
    [SerializeField] private Button[] itemButtons;

    private int currentIndex = 0;

    private void Start()
    {
        UpdateInteractableSelected(itemButtons[currentIndex].gameObject);
        /*for (int i = 0; i < itemButtons.Length; i++)
        {
            int buttonIndex = i;
            itemButtons[i].onClick.AddListener(() => ClickedButton(buttonIndex));
        }*/
    }

    #region Events

    private void OnEnable()
    {
        gameManager.OnNewItem += AddNewItem;
        gameManager.OnUpdateItemSelected += UpdateItemSelected;
    }

    private void AddNewItem(ItemSO itemData)
    {
        for (int i = 0; i < itemButtons.Length; i++)
        {
            if (itemButtons[i].IsActive())
            {
                itemButtons[i].GetComponent<Image>().sprite = itemData.ItemIcon;
                itemButtons[i].gameObject.SetActive(true);
                UpdateInteractableSelected(itemButtons[i].gameObject);
                currentIndex = i;
                gameManager.NewItemAdded(true);
                return;
            }
        }
        gameManager.NewItemAdded(false);
    }

    private void UpdateItemSelected(int scrollDirection)
    {
        int newIndex = currentIndex + scrollDirection;
        if (newIndex >= 0 && newIndex < itemButtons.Length && itemButtons[newIndex].IsActive())
        {
            currentIndex = newIndex;
            UpdateInteractableSelected(itemButtons[currentIndex].gameObject);
        }
    }

    private void OnDisable()
    {
        gameManager.OnNewItem -= AddNewItem;
        gameManager.OnUpdateItemSelected -= UpdateItemSelected;
    }

    #endregion

    public void UpdateInteractableSelected(GameObject newSelection)
    {
        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(newSelection, new BaseEventData(eventSystem));
    }

    /*private void ClickedButton(int buttonIndex)
    {

    }*/
}
