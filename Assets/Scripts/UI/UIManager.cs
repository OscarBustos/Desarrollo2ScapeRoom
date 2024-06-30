using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject interactionPanel;
    [SerializeField] private TextMeshProUGUI interactionText;

    #region Events
    private void OnEnable()
    {
        EventManager.OnShowInteraction += ShowInteraction;
        EventManager.OnHideInteraction += HideInteraction;
    }

    private void OnDisable()
    {
        EventManager.OnShowInteraction -= ShowInteraction;
        EventManager.OnHideInteraction -= HideInteraction;
    }
    #endregion

    private void Start()
    {
        interactionPanel.SetActive(false);
    }

    private void ShowInteraction(string text)
    {
        interactionText.text = text;
        interactionPanel.SetActive(true);
    }

    private void HideInteraction()
    {
        interactionPanel.SetActive(false);
    }

    private void UpdateInteractionText()
    {
        throw new NotImplementedException();
    }
}
