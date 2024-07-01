using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Interaction UI")]

    [SerializeField] 
    private GameObject reticle;

    [SerializeField] 
    private GameObject interactionPanel;

    [SerializeField] 
    private TextMeshProUGUI interactionText;

    [SerializeField] 
    private bool alwaysShowReticle = true;

    private CanvasGroup reticleCanvasGroup;
    private CanvasGroup interactionPanelCanvasGroup;

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

    private void Awake()
    {
        reticleCanvasGroup = reticle.GetComponent<CanvasGroup>();
        interactionPanelCanvasGroup = interactionPanel.GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        if (!alwaysShowReticle) reticleCanvasGroup.alpha = 0f;
        interactionPanelCanvasGroup.alpha = 0f;
    }

    private void ShowInteraction(string text)
    {
        interactionText.text = text;

        StartCoroutine(FadeCanvasGroup(reticleCanvasGroup, 1f));
        StartCoroutine(FadeCanvasGroup(interactionPanelCanvasGroup, 1f));
    }

    private void HideInteraction()
    {
        StartCoroutine(FadeCanvasGroup(reticleCanvasGroup, 0f));
        StartCoroutine(FadeCanvasGroup(interactionPanelCanvasGroup, 0f));
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float targetAlpha, float duration = 0.25f)
    {
        float startAlpha = canvasGroup.alpha;
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, (Time.time - startTime) / duration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}
