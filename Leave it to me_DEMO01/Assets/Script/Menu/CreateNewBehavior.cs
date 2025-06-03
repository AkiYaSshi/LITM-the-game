using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateNewBehavior : MonoBehaviour
{
    [Tooltip("一開始即顯示的物件")]
    [SerializeField]
    private GameObject originPanel;

    [Tooltip("之後出現的物件")]
    [SerializeField]
    private GameObject newPanel;

    [Tooltip("目標按鈕")]
    [SerializeField]
    private Button Button;

    [Tooltip("按下後便回到初始狀態的物件")]
    [SerializeField]
    private List<Button> buttons;

    private void Start()
    {
        UndoDisplay();
    }

    private void SwitchDisplay()
    {
        originPanel.SetActive(false);
        newPanel.SetActive(true);
        Button.enabled = false;
    }

    public void UndoDisplay()
    {
        originPanel.SetActive(true);
        newPanel.SetActive(false);
        Button.enabled = true;
    }
    private void OnEnable()
    {
        Button.onClick.AddListener(SwitchDisplay);
        foreach (var button in buttons)
        {
            button.onClick.AddListener(UndoDisplay);
        }
    }
    private void OnDisable()
    {
        Button.onClick.RemoveListener(SwitchDisplay);
        foreach (var button in buttons)
        {
            button.onClick.RemoveListener(UndoDisplay);
        }
    }
}
