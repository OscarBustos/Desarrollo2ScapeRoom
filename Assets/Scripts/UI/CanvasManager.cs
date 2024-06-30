using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private Button[] buttons;

    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i;
            buttons[i].onClick.AddListener(() => ClickedButton(buttonIndex));
        }
    }

    private void ClickedButton(int buttonIndex)
    {

    }
}
