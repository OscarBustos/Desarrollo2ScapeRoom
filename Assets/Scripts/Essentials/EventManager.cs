using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static event Action<string> OnShowInteraction;
    public static event Action OnHideInteraction;

    public static void ShowInteraction(string text)
    {
        OnShowInteraction?.Invoke(text);
    }

    public static void HideInteraction()
    {
        OnHideInteraction?.Invoke();
    }
}
